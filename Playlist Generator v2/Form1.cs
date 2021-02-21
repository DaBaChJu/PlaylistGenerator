using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Playlist_Generator_v2
{

	/// <summary>
	/// Le tri par priorité ne marche pas dans le sens où il n'est
	/// pas mis à jour quand les priorités changent après le
	/// chargement initial.A réparer
	/// </summary>


	public partial class Form1 : Form
	{

		/// <summary>
		/// Boite de dialogue "A propos".Créée 1 seule fois au démarrage.
		/// </summary>
		aboutBox boite_dialogue_a_propos = new aboutBox();

		/// <summary>
		/// Liste des caractères interdits dans un nom de fichier.
		/// Généré une seule fois au démarrage.
		/// </summary>
		char[] caractères_interdits_nom_de_fichier = Path.GetInvalidFileNameChars();

		/// <summary>
		/// Liste des caractères interdits dans un chemin de fichier.
		/// Généré une seule fois au démarrage.
		/// </summary>
		char[] caractères_interdits_chemin_de_fichier = Path.GetInvalidPathChars();

		#region Données membres

		/// <summary>
		/// Liste de gauche,1 enregistrement par fichier,notion de priorité.
		/// Triée par nom de fichier.
		/// </summary>
		List<Fichier> liste_fichiers = null;

		/// <summary>
		/// Liste de gauche,1 enregistrement par fichier,notion de priorité.
		/// Triée par priorité.
		/// </summary>
		List<Fichier> liste_fichiers_priotri = null;

		/// <summary>
		/// Liste de gauche,version utilisée pour autocomplétion
		/// </summary>
		AutoCompleteStringCollection liste_saisie_semiauto = new AutoCompleteStringCollection();

		/// <summary>
		/// Liste de droite,liste finale à enregistrer,fichiers présents autant de fois que leur priorité.
		/// </summary>
		List<Fichier> liste_fichiers_finale = null;

		/// <summary>
		/// Indique si la liste de droite est à jour
		/// </summary>
		bool bListeGénérée;

		/// <summary>
		/// Indique quelle liste a eu le focus en dernier.
		/// False=liste de gauche (par défaut)
		/// True=liste de droite
		/// </summary>
		bool bListeDroiteDernièreFocusée = false;

		/// <summary>
		/// Nombre total de fichiers qui seront dans la liste finale
		/// </summary>
		int nPrioritéTotale;

		/// <summary>
		/// Playlist actuellement ouverte ou nom par défaut
		/// </summary>
		string playlist_courante = "playlist.m3u";

		/// <summary>
		/// Boite de dialogue d'ouverture globale
		/// </summary>
		OpenFileDialog open_file_dialog = new OpenFileDialog();

		/// <summary>
		/// Boite de dialogue d'enregistrement globale
		/// </summary>
		SaveFileDialog save_file_dialog = new SaveFileDialog();


		#endregion Données membres

		//---------------------------------------------------------------//
		//---------------------------------------------------------------//

		#region Constructeur

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="args">Arguments</param>

		public Form1(string[] args)
		{

			InitializeComponent();

			if (args != null && args.Length != 0)
				OuvrirPlaylist(args[0]);
			else
				OuvrirPlaylist(null);

			GUI_textBox_searchbox.AutoCompleteCustomSource = liste_saisie_semiauto;

			#region Configuration des FileDialog

			open_file_dialog.CheckFileExists = true;

			save_file_dialog.AddExtension = true;
			save_file_dialog.CheckPathExists = true;
			save_file_dialog.DefaultExt = "m3u";
			save_file_dialog.Filter = "Playlistes (*.m3u)|*.m3u|Tous fichiers (*.*)|*.*";
			save_file_dialog.FilterIndex = 1;
			save_file_dialog.OverwritePrompt = true;

			#endregion Configuration des FileDialog

		}

		#endregion Constructeur

		#region Drag&Drop

		#region DragEnter générique

		/// <summary>
		/// Fonction DragEnter générique.
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void GUI_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
		}

		#endregion DragEnter générique

		#region DragDrop pour la liste de gauche

		/// <summary>
		/// Fonction DragDrop pour la liste de gauche.
		/// Pour charger des fichiers pour playlist.
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void GUI_listBox_liste_de_gauche_DragDrop(object sender, DragEventArgs e)
		{

			string[] liste_dragNdrop = (string[]) e.Data.GetData(DataFormats.FileDrop);

			if (liste_dragNdrop != null && liste_dragNdrop.Length != 0)
			{

				foreach (string chemin_fichier in liste_dragNdrop)
					AjouteFichier(chemin_fichier);

				MajInterface();
				bListeGénérée = false;

			}

		}

		#endregion DragDrop pour la liste de gauche

		#region DragDrop pour la fenêtre

		/// <summary>
		/// Fonction DragDrop pour la fenêtre.
		/// Pour charger des playlistes.
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>
		
		private void Form1_DragDrop(object sender, DragEventArgs e)
		{

			DialogResult choix;
			string[] liste_dragNdrop = (string[]) e.Data.GetData(DataFormats.FileDrop);

			if (liste_dragNdrop != null && liste_dragNdrop.Length == 1)
			{

					choix = MessageBox.Show(string.Format("Abandonner les listes en cours pour charger la liste\r\n?",liste_dragNdrop[0]),
											string.Format("Ouvrir playlist {0}", Path.GetFileName(liste_dragNdrop[0])), 
											MessageBoxButtons.OKCancel, 
											MessageBoxIcon.Exclamation
					);

			}
			else
				choix = DialogResult.Cancel;

			if (choix == DialogResult.OK)
			{
				OuvrirPlaylist(liste_dragNdrop[0]);
			}

		}

		#endregion DragDrop pour la fenêtre

		#endregion Drag&Drop

		#region Boite de dialogue "A propos"

		/// <summary>
		/// Sélection de ?/A propos ou appuyé sur F1
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
		{
			boite_dialogue_a_propos.ShowDialog();
		}

		#endregion Boite de dialogue "A propos"

		#region Fichier/Nouveau

		/// <summary>
		/// Sélection de Fichier/Nouveau ou appuyé sur Ctrl+N
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void nouveauToolStripMenuItem_Click(object sender, EventArgs e)
		{

			DialogResult choix = MessageBox.Show("Ceci effacera les listes courantes!", "Nouveau", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

			if (choix == DialogResult.OK)
			{

				GUI_listBox_liste_de_gauche.Items.Clear();
				GUI_listBox_liste_de_droite.Items.Clear();
				liste_fichiers.Clear();
				liste_fichiers_priotri.Clear();
				liste_saisie_semiauto.Clear();
				nPrioritéTotale = 0;
				playlist_courante = "playlist.m3u";

				MajInterface();

				bListeGénérée = false;

			}

		}

		#endregion Fichier/Nouveau

		#region Fichier/Ouvrir

		/// <summary>
		/// Sélection de Fichier/Ouvrir ou appuyé sur Ctrl+O
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void ouvrirUnePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
		{

			DialogResult choix;

			if (liste_fichiers==null || liste_fichiers.Count == 0)
				choix = DialogResult.OK;
			else
				choix = MessageBox.Show("Les listes actuelles seront perdues,continuer?", "Ouvrir", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

			if (choix == DialogResult.OK)
			{

				if (open_file_dialog.RestoreDirectory != true)
				{
					open_file_dialog.Filter = "Playlistes (*.m3u)|*.m3u|Tous fichiers (*.*)|*.*";
					open_file_dialog.FilterIndex = 1;
					open_file_dialog.Multiselect = false;
					open_file_dialog.SupportMultiDottedExtensions = false;
					open_file_dialog.RestoreDirectory = true;
				}

				open_file_dialog.FileName = playlist_courante;

				if (open_file_dialog.ShowDialog() == DialogResult.OK)
					OuvrirPlaylist(open_file_dialog.FileName);

			}

		}

		#endregion Fichier/Ouvrir

		#region Fichier/Ajouter des vidéos (aussi depuis menu contextuel)

		/// <summary>
		/// Sélection de Fichier/Ajouter des vidéos ou menu contextuel/Ajouter des vidéos
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void ajouterDesVidéosToolStripMenuItem_Click(object sender, EventArgs e)
		{

			if (open_file_dialog.Multiselect != true)
			{

				open_file_dialog.Filter = "Tous fichiers (*.*)|*.*";
				open_file_dialog.FilterIndex = 1;
				open_file_dialog.Multiselect = true;
				open_file_dialog.SupportMultiDottedExtensions = true;
				open_file_dialog.RestoreDirectory = false;

			}

			if (open_file_dialog.ShowDialog() == DialogResult.OK)
			{

				//mise à jour manuelle de la capacité,car les fichiers sont ajoutés 1 par 1 et ralentiraient le processus
				if (liste_fichiers.Capacity < liste_fichiers.Count + open_file_dialog.FileNames.Length + 1)
				{
					liste_fichiers.Capacity = liste_fichiers.Count + ((open_file_dialog.FileNames.Length / 10) + 1) * 10;
					liste_fichiers_priotri.Capacity = liste_fichiers.Capacity;
				}

				foreach (string fichier_traité in open_file_dialog.FileNames)
					AjouteFichier(fichier_traité);

				MajInterface();
				bListeGénérée = false;

			}

		}

		#endregion Fichier/Ajouter des vidéos (aussi depuis menu contextuel)

		#region Fichier/Enregistrer Sous

		/// <summary>
		/// Sélection de Fichier/Enregistrer Sous ou appuyé sur Ctrl+S
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void enregistrerSousToolStripMenuItem_Click(object sender, EventArgs e)
		{

			if (!bListeGénérée || liste_fichiers_finale == null)
				MessageBox.Show("Vous devez générer une liste pour l'enregistrer.", "Enregistrer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else
			{

				save_file_dialog.FileName = playlist_courante;

				if (save_file_dialog.ShowDialog() == DialogResult.OK)
				{

					StreamWriter fichier = new StreamWriter(save_file_dialog.FileName);
					foreach (Fichier ligne_playlist in liste_fichiers_finale)
						fichier.WriteLine(ligne_playlist.chemin_complet);
					fichier.Close();

					playlist_courante = save_file_dialog.FileName;

				}

			}

		}

		#endregion Fichier/Enregistrer Sous

		#region Fichier/Quitter

		/// <summary>
		/// Sélection de Fichier/Quitter
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion Quitter

		#region Bouton Edit/Ok

		/// <summary>
		/// Bouton permettant d'éditer le chemin de fichier en cours
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void bouton_éditer_Click(object sender, EventArgs e)
		{

			ListBox GUI_liste_raccourci=null;

			string str_chemin_courant=null;
			string str_nom_fichier_courant=null;

			int nIndex = -1;

			#region Identification de la liste sélectionnée et allocation des variables en fonction de cela
			if (!bListeDroiteDernièreFocusée) 
			{

				GUI_liste_raccourci = GUI_listBox_liste_de_gauche;
				nIndex = GUI_listBox_liste_de_gauche.SelectedIndex;

			}
			else
			{

				GUI_liste_raccourci = GUI_listBox_liste_de_droite;
				nIndex = GUI_listBox_liste_de_droite.SelectedIndex;
			}
			#endregion Identification de la liste sélectionnée et allocation des variables en fonction de cela

			if (nIndex != -1)
			{

				if (GUI_button_bouton_éditer.Text == "Edit")
				{
					GUI_textBox_nom_chemin_fichier.ReadOnly = false;
					GUI_button_bouton_éditer.Text = "Ok";
				}
				else
				{

					int i_nom = -1;
					int i_chemin = -1;
					bool bWrongCharFound = false;

					#region Sépare le chemin et le nom de fichier puis i++,peut détecter rapidement certaines erreurs
					try
					{
						str_chemin_courant = Path.GetDirectoryName(GUI_textBox_nom_chemin_fichier.Text);
						str_nom_fichier_courant = Path.GetFileName(GUI_textBox_nom_chemin_fichier.Text);
						i_chemin++;
					}
					catch (Exception ex)
					{
						MessageBox.Show(
							"Des éléments invalides ont été trouvés.Voici le détail de l'erreur:\r\n" + ex.Message,
							"Erreur"
						);
						bWrongCharFound = true;
					}
					#endregion Sépare le chemin et le nom de fichier puis i++,peut détecter rapidement certaines erreurs

					while (!bWrongCharFound && i_chemin < str_chemin_courant.Length)
					{
						if (Array.IndexOf(caractères_interdits_chemin_de_fichier, str_chemin_courant[i_chemin]) != -1)
							bWrongCharFound = true;
						else
							i_chemin++;
					}

					if (!bWrongCharFound)
					{

						i_nom = 0;

						while (i_nom < str_nom_fichier_courant.Length && !bWrongCharFound)
						{
							if (Array.IndexOf(caractères_interdits_nom_de_fichier, str_nom_fichier_courant[i_nom]) != -1)
								bWrongCharFound = true;
							else
								i_nom++;
						}

					}

					if (bWrongCharFound)
					{

						if (i_nom >= 0)
							MessageBox.Show(
								string.Format("Le caractère {0} est interdit.", str_nom_fichier_courant[i_nom]),
								"Erreur"
							);
						else
						if (i_chemin >= 0)
							MessageBox.Show(
								string.Format("Le caractère {0} est interdit.", str_chemin_courant[i_nom]),
								"Erreur"
							);
					}
					else
					{

						GUI_textBox_nom_chemin_fichier.ReadOnly = true;
						GUI_button_bouton_éditer.Text = "Edit";

						if (!bListeDroiteDernièreFocusée)
						{

							int nPriorité;
							int nIndex_liste_gauche_alternative;

							if (GUI_checkBox_tri_par_priorité.CheckState == CheckState.Checked)
							{

								//récupération de l'index de la liste non sélectionnée (tri par priorité ou alpha)
								nIndex_liste_gauche_alternative = liste_fichiers.IndexOf(liste_fichiers_priotri[nIndex]);

								//récupération de la priorité
								nPriorité = liste_fichiers_priotri[nIndex].priorite;

								//suppression des anciennes entrées
								liste_fichiers.RemoveAt(nIndex_liste_gauche_alternative);
								liste_fichiers_priotri.RemoveAt(nIndex);
								liste_saisie_semiauto.RemoveAt(nIndex_liste_gauche_alternative);
								GUI_listBox_liste_de_gauche.Items.RemoveAt(nIndex);

								//màj priorité totale
								nPrioritéTotale -= nPriorité;

								//Ajout de la nouvelle entrée
								nIndex=AjouteFichier(GUI_textBox_nom_chemin_fichier.Text, nPriorité);

								//Mise à jour de la sélection dans la liste graphique
								GUI_liste_raccourci.SelectedIndex = nIndex;

								//Mise à jour interface pour cause changement de sélection
								MajInterface();

								/*
								liste_fichiers_priotri[nIndex] = new Fichier(GUI_textBox_nom_chemin_fichier.Text);
								liste_fichiers[nIndex_liste_gauche_alternative] = liste_fichiers_priotri[nIndex];
								liste_saisie_semiauto[nIndex_liste_gauche_alternative] = liste_fichiers[nIndex_liste_gauche_alternative].nom;
								GUI_liste_raccourci.Items[nIndex] = liste_fichiers_priotri[nIndex].nom;
								 * */

							}
							else
							{

								//récupération de l'index de la liste non sélectionnée (tri par priorité ou alpha)
								nIndex_liste_gauche_alternative = liste_fichiers_finale.IndexOf(liste_fichiers[nIndex]);

								//récupération de la priorité
								nPriorité = liste_fichiers[nIndex].priorite;

								//suppression des anciennes entrées
								liste_fichiers.RemoveAt(nIndex);
								liste_fichiers_priotri.RemoveAt(nIndex_liste_gauche_alternative);
								liste_saisie_semiauto.RemoveAt(nIndex);
								GUI_listBox_liste_de_gauche.Items.RemoveAt(nIndex);

								//màj priorité totale
								nPrioritéTotale -= nPriorité;

								//Ajout de la nouvelle entrée
								nIndex = AjouteFichier(GUI_textBox_nom_chemin_fichier.Text, nPriorité);

								//Mise à jour de la sélection dans la liste graphique
								GUI_liste_raccourci.SelectedIndex = nIndex;

								//Mise à jour interface pour cause changement de sélection
								MajInterface();

								////récupération de la priorité
								////nPriorité = liste_fichiers[nIndex].priorite;

								//nIndex_liste_gauche_alternative = liste_fichiers_priotri.IndexOf(liste_fichiers[nIndex]);

								////Suppression de l'ancien élément
								//GUI_liste_raccourci.Items.RemoveAt(nIndex);
								//liste_fichiers.RemoveAt(nIndex);
								//liste_fichiers_priotri.RemoveAt(nIndex_liste_gauche_alternative);

								////Rajout du nouvel élément
								//AjouteFichier(GUI_textBox_nom_chemin_fichier.Text);

								////liste_fichiers[nIndex] = new Fichier(GUI_textBox_nom_chemin_fichier.Text);
								////liste_saisie_semiauto[nIndex] = liste_fichiers[nIndex].nom;
								////liste_fichiers_priotri[nIndex_liste_gauche_alternative] = liste_fichiers[nIndex];
								////GUI_liste_raccourci.Items[nIndex] = liste_fichiers[nIndex].nom;

							}

						}
						else
						{
							liste_fichiers_finale[nIndex] = new Fichier(GUI_textBox_nom_chemin_fichier.Text);
							GUI_liste_raccourci.Items[nIndex] = liste_fichiers_finale[nIndex].nom;
						}

					}

				}

			}

		}

		#endregion Bouton Edit/Ok

		#region Bouton d'augmentation de la priorité

		/// <summary>
		/// Bouton + permettant d'augmenter la priorité
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void GUI_button_plus_Click(object sender, EventArgs e)
		{

			int nombre;
			if (int.TryParse(GUI_textBox_champ_priorité.Text, out nombre))
				GUI_textBox_champ_priorité.Text = (nombre + 1).ToString();

			bListeGénérée = false;

		}

		#endregion Bouton d'augmentation de la priorité

		#region Bouton de diminution de la priorité

		/// <summary>
		/// Bouton - permettant de diminuer la priorité
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void GUI_button_moins_Click(object sender, EventArgs e)
		{

			int nombre;
			if (int.TryParse(GUI_textBox_champ_priorité.Text, out nombre) && nombre != 0)
				GUI_textBox_champ_priorité.Text = (nombre - 1).ToString();

			bListeGénérée = false;

		}

		#endregion Bouton de diminution de la priorité

		#region Bouton valider

		/// <summary>
		/// Bouton qui valide les modifications de la priorité ou du pourcentage
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void GUI_button_bouton_valider_Click(object sender, EventArgs e)
		{

			int nIndex = GUI_listBox_liste_de_gauche.SelectedIndex;
			int nIndex_alternatif=-1;
			int nValeur;
			bool bRéussi = int.TryParse(GUI_textBox_champ_priorité.Text, out nValeur);

			List<Fichier> liste_en_cours = null;
			List<Fichier> liste_alternative = null;

			if (nIndex != -1)
			{

				if (GUI_checkBox_tri_par_priorité.CheckState == CheckState.Checked)
				{
					liste_en_cours = liste_fichiers_priotri;
					liste_alternative = liste_fichiers;
					nIndex_alternatif = liste_fichiers.IndexOf(liste_fichiers_priotri[nIndex]);
				}
				else
				{
					liste_en_cours = liste_fichiers;
					liste_alternative = liste_fichiers_priotri;
					nIndex_alternatif = liste_fichiers_priotri.IndexOf(liste_fichiers[nIndex]);
				}

				//modification de la priorité (sinon mod pourcentage)
				if (bRéussi && nValeur != liste_en_cours[nIndex].priorite)
				{

					//récupération de la priorité
					int nPriorité = liste_en_cours[nIndex].priorite;

					//récupération du chemin
					string chemin_fichier = liste_en_cours[nIndex].chemin_complet;

					//suppression des anciennes entrées
					liste_alternative.RemoveAt(nIndex_alternatif);
					liste_en_cours.RemoveAt(nIndex);
					GUI_listBox_liste_de_gauche.Items.RemoveAt(nIndex);

					if (GUI_checkBox_tri_par_priorité.CheckState == CheckState.Checked)
						liste_saisie_semiauto.RemoveAt(nIndex_alternatif);
					else
						liste_saisie_semiauto.RemoveAt(nIndex);

					//màj priorité totale
					nPrioritéTotale -= nPriorité;

					//Ajout de la nouvelle entrée
					nIndex = AjouteFichier(chemin_fichier, nValeur);

					//Mise à jour de la sélection dans la liste graphique
					GUI_listBox_liste_de_gauche.SelectedIndex = nIndex;

					//Mise à jour interface pour cause changement de sélection
					MajInterface();

				}
				else
				{

					//si la priorité n'a pas changé,vérifie le pourcentage et met à jour si nécessaire
					bRéussi = int.TryParse(GUI_textBox_pourcentage.Text, out nValeur);

					if (bRéussi && nValeur != (100 * liste_en_cours[nIndex].priorite / nPrioritéTotale))
					{

						if (nValeur < 0 || nValeur >= 100)
							MessageBox.Show(
								"Le pourcentage entré est trop élevé.\r\nLe calcul ne sera pas effectué.",
								"Calcul impossible",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
							);
						else
							if (nValeur == 0) //si pourcentage mis à 0,priorité réduite à 1
							{

								//récupération de la priorité
								int nPriorité = liste_en_cours[nIndex].priorite;

								//récupération du chemin
								string chemin_fichier = liste_en_cours[nIndex].chemin_complet;

								//suppression des anciennes entrées
								liste_alternative.RemoveAt(nIndex_alternatif);
								liste_en_cours.RemoveAt(nIndex);
								GUI_listBox_liste_de_gauche.Items.RemoveAt(nIndex);

								if (GUI_checkBox_tri_par_priorité.CheckState == CheckState.Checked)
									liste_saisie_semiauto.RemoveAt(nIndex_alternatif);
								else
									liste_saisie_semiauto.RemoveAt(nIndex);

								//màj priorité totale
								nPrioritéTotale = nPrioritéTotale - liste_en_cours[nIndex].priorite;

								//Ajout de la nouvelle entrée
								nIndex = AjouteFichier(chemin_fichier,1);

								//Mise à jour de la sélection dans la liste graphique
								GUI_listBox_liste_de_gauche.SelectedIndex = nIndex;

								//Mise à jour interface pour cause changement de sélection
								MajInterface();

							}
							else
							{

								//récupération de la priorité
								int nPriorité = liste_en_cours[nIndex].priorite;

								//récupération du chemin
								string chemin_fichier = liste_en_cours[nIndex].chemin_complet;

								//calcul de la valeur à ajouter pour atteindre la nouvelle priorité totale
								int x = (nValeur * nPrioritéTotale - 100 * liste_en_cours[nIndex].priorite) / (100 - nValeur);	//cf fin fichier

								//suppression des anciennes entrées
								liste_alternative.RemoveAt(nIndex_alternatif);
								liste_en_cours.RemoveAt(nIndex);
								GUI_listBox_liste_de_gauche.Items.RemoveAt(nIndex);

								if (GUI_checkBox_tri_par_priorité.CheckState == CheckState.Checked)
									liste_saisie_semiauto.RemoveAt(nIndex_alternatif);
								else
									liste_saisie_semiauto.RemoveAt(nIndex);

								//màj priorité totale
								nPrioritéTotale = nPrioritéTotale - nPriorité;

								//Ajout de la nouvelle entrée
								nIndex = AjouteFichier(chemin_fichier, nPriorité + x);

								//Mise à jour de la sélection dans la liste graphique
								GUI_listBox_liste_de_gauche.SelectedIndex = nIndex;

								//Mise à jour interface pour cause changement de sélection
								MajInterface();

							}

						MajInterface();

					}

				}

				bListeGénérée = false;

			}

		}

		#endregion Bouton valider

		#region Clic sur Tri par priorité

		/// <summary>
		/// Change le tri de la liste de gauche.
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void GUI_checkBox_tri_par_priorité_CheckedChanged(object sender, EventArgs e)
		{

			int nIndex = GUI_listBox_liste_de_gauche.SelectedIndex;

			GUI_listBox_liste_de_gauche.Items.Clear();

			if (GUI_checkBox_tri_par_priorité.CheckState == CheckState.Checked)
			{

				foreach (Fichier fichier_lu in liste_fichiers_priotri)
					GUI_listBox_liste_de_gauche.Items.Add(fichier_lu.nom);

				if(nIndex != -1)
					GUI_listBox_liste_de_gauche.SelectedIndex =
						liste_fichiers_priotri.IndexOf(liste_fichiers[nIndex]);

			}
			else
			{

				foreach (Fichier fichier_lu in liste_fichiers)
					GUI_listBox_liste_de_gauche.Items.Add(fichier_lu.nom);

				if (nIndex != -1)
					GUI_listBox_liste_de_gauche.SelectedIndex=
						liste_fichiers.IndexOf(liste_fichiers_priotri[nIndex]);

			}

		}

		#endregion Clic sur Tri par priorité

		#region Bouton Générer liste

		/// <summary>
		/// Bouton qui génère la liste de droite,utilisée pour créer la playlist
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void GUI_button_bouton_générer_Click(object sender, EventArgs e)
		{

			if (GUI_listBox_liste_de_gauche.Items.Count != 0)
			{

				GUI_listBox_liste_de_droite.Items.Clear();

				if (liste_fichiers_finale != null)
					liste_fichiers_finale.Clear();

				liste_fichiers_finale = new List<Fichier>(nPrioritéTotale + 1);

				int nIndex = 0;

				foreach (Fichier enregistrement_liste_gauche in liste_fichiers)
				{

					int nValeur = 0;

					while (nValeur < enregistrement_liste_gauche.priorite)
					{

						liste_fichiers_finale.Add(enregistrement_liste_gauche);
						nIndex++;

						GUI_listBox_liste_de_droite.Items.Add(enregistrement_liste_gauche.nom);
						nValeur++;

					}

				}

				bListeGénérée = true;

			}

		}

		#endregion Bouton Générer liste

		#region Bouton Randomiser liste

		/// <summary>
		/// Bouton qui réorganise aléatoirement la liste de droite,utilisée pour créer la playlist.
		/// Algorithme légèrement différent du programme d'origine.
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void GUI_button_bouton_random_Click(object sender, EventArgs e)
		{

			if (bListeGénérée)
			{

				LinkedList<Fichier> nouvelle_liste = new LinkedList<Fichier>();
				Random RNG = new Random();
				int nVraiCount = liste_fichiers_finale.Count;

				Fichier fichier_vide = new Fichier();

				//génère une 1ère valeur pour initialiser
				RNG.Next();

				GUI_listBox_liste_de_droite.Items.Clear();

				while (nVraiCount != 0)
				{

					int nIndexVoulu= RNG.Next(liste_fichiers_finale.Count);

					while (liste_fichiers_finale[nIndexVoulu] == fichier_vide)
						nIndexVoulu = RNG.Next(liste_fichiers_finale.Count);

					nouvelle_liste.AddLast(liste_fichiers_finale[nIndexVoulu]);
					GUI_listBox_liste_de_droite.Items.Add(liste_fichiers_finale[nIndexVoulu].nom);
					liste_fichiers_finale[nIndexVoulu] = fichier_vide;
					nVraiCount--;

				}

				liste_fichiers_finale.Clear();
				liste_fichiers_finale = new List<Fichier>(nouvelle_liste);

			}
			else
				MessageBox.Show("Vous devez regénérer la liste avant de Randomiser.", "Liste non mise à jour", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

		}

		#endregion Bouton Randomiser liste

		#region Bouton "Flèche vers le haut"

		/// <summary>
		/// Bouton qui remonte un élément de la liste de droite.
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void GUI_button_bouton_haut_Click(object sender, EventArgs e)
		{

			if (liste_fichiers_finale!=null && liste_fichiers_finale.Count > 1)
			{

				int nIndex = GUI_listBox_liste_de_droite.SelectedIndex;

				if (nIndex - 1 >= 0)
				{

					Fichier temp_fichier = liste_fichiers_finale[nIndex];

					liste_fichiers_finale[nIndex] = liste_fichiers_finale[nIndex-1];
					liste_fichiers_finale[nIndex - 1] = temp_fichier;

					GUI_listBox_liste_de_droite.Items.RemoveAt(nIndex);
					GUI_listBox_liste_de_droite.Items.RemoveAt(nIndex - 1);

					GUI_listBox_liste_de_droite.Items.Insert(nIndex-1,liste_fichiers_finale[nIndex-1].nom);
					GUI_listBox_liste_de_droite.Items.Insert(nIndex,liste_fichiers_finale[nIndex].nom);

					GUI_listBox_liste_de_droite.SelectedIndex = nIndex - 1;

				}
			}

		}

		#endregion Bouton "Flèche vers le haut"

		#region Bouton "Flèche vers le bas"

		/// <summary>
		/// Bouton qui redescend un élément de la liste de droite.
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void GUI_button_bouton_bas_Click(object sender, EventArgs e)
		{

			if (liste_fichiers_finale != null && liste_fichiers_finale.Count > 1)
			{

				int nIndex = GUI_listBox_liste_de_droite.SelectedIndex;

				if (nIndex + 1 < GUI_listBox_liste_de_droite.Items.Count)
				{

					Fichier temp_fichier = liste_fichiers_finale[nIndex];

					liste_fichiers_finale[nIndex] = liste_fichiers_finale[nIndex + 1];
					liste_fichiers_finale[nIndex + 1] = temp_fichier;

					GUI_listBox_liste_de_droite.Items.RemoveAt(nIndex + 1);
					GUI_listBox_liste_de_droite.Items.RemoveAt(nIndex);

					GUI_listBox_liste_de_droite.Items.Insert(nIndex, liste_fichiers_finale[nIndex].nom);
					GUI_listBox_liste_de_droite.Items.Insert(nIndex + 1, liste_fichiers_finale[nIndex + 1].nom);

					GUI_listBox_liste_de_droite.SelectedIndex = nIndex + 1;

				}
			}

		}

		#endregion Bouton "Flèche vers le bas"

		#region Saisie semi-automatique liste de gauche touche Entrée

		/// <summary>
		/// Touche appuyée pendant que la liste de gauche est sélectionnée,
		/// géré pour la saisie semi-automatique pour gérer la touche Entrée.
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre contenant l'information sur la touche.
		/// appuyée.</param>

		private void GUI_listBox_liste_de_gauche_KeyUp(object sender, KeyEventArgs e)
		{

			if (e.KeyCode == Keys.Enter && GUI_textBox_searchbox.Visible)
			{

				if (GUI_listBox_liste_de_gauche.Items.Count > 0)
				{

					bool bFound = false;
					int nIndex = GUI_listBox_liste_de_gauche.SelectedIndex + 1;

					//recherche la chaine suivante contenant la combinaison de touche recherchée
					while (!bFound && nIndex < GUI_listBox_liste_de_gauche.Items.Count)
					{

						if ((GUI_listBox_liste_de_gauche.Items[nIndex] as string).Contains(GUI_textBox_searchbox.Text))
							bFound = true;
						else
							nIndex++;

					}

					//non trouvé en partant de la sélection courante,recherche depuis le début
					if (nIndex == GUI_listBox_liste_de_gauche.Items.Count)
					{

						nIndex = 0;

						while (!bFound && nIndex < GUI_listBox_liste_de_gauche.SelectedIndex)
						{

							if ((GUI_listBox_liste_de_gauche.Items[nIndex] as string).Contains(GUI_textBox_searchbox.Text))
								bFound = true;
							else
								nIndex++;

						}

					}

					//met à jour l'index si trouvé
					if (nIndex != GUI_listBox_liste_de_gauche.SelectedIndex)
						GUI_listBox_liste_de_gauche.SelectedIndex = nIndex;
					else
						System.Media.SystemSounds.Exclamation.Play();

					e.Handled = true;

				}
				else
					System.Media.SystemSounds.Beep.Play();

			}
			else
				e.Handled = false;

		}

		#endregion Saisie semi-automatique liste de gauche touche Entrée

		#region Saisie semi-automatique liste de gauche Caractères

		/// <summary>
		/// Touche appuyée pendant que la liste de gauche est sélectionnée,
		/// géré pour la saisie semi-automatique pour gérer les caractères.
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre contenant l'information sur la touche.
		/// appuyée.</param>

		private void GUI_listBox_liste_de_gauche_KeyPress(object sender, KeyPressEventArgs e)
		{

			GUI_textBox_searchbox.Visible = true;

			//si -1,le caractère ne fait pas partie des caractères interdits
			if (Array.IndexOf(caractères_interdits_nom_de_fichier, e.KeyChar) == -1)
			{
				GUI_textBox_searchbox.Text = GUI_textBox_searchbox.Text + e.KeyChar;
				e.Handled = true;
			}
			else
			{
				System.Media.SystemSounds.Beep.Play();
				e.Handled = false;
			}

		}

		#endregion Saisie semi-automatique liste de gauche Caractères

		#region Nettoyage saisie semi-automatique

		/// <summary>
		/// Quitte la liste de gauche,remettant à zéro la saisie semi-automatique.
		/// Enregistre aussi que la dernière liste visitée est celle de gauche au
		/// cas où le bouton Edit serait pressé.
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void GUI_listBox_liste_de_gauche_Leave(object sender, EventArgs e)
		{
			GUI_textBox_searchbox.Clear();
			GUI_textBox_searchbox.Visible = false;
			bListeDroiteDernièreFocusée = false;
		}

		#endregion Nettoyage saisie semi-automatique

		#region Perte focus liste droite

		/// <summary>
		/// Liste de droite a perdu le focus.Au cas où le bouton edit serait
		/// pressé,retiens que la dernière liste sélectionnée est celle de droite.
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void GUI_listBox_liste_de_droite_Leave(object sender, EventArgs e)
		{
			bListeDroiteDernièreFocusée = true;
		}

		#endregion Perte focus liste droite

		#region Sélection dans une liste changée

		/// <summary>
		/// La sélection de la liste de gauche a changé.
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void GUI_listBox_liste_de_gauche_SelectedIndexChanged(object sender, EventArgs e)
		{
			MajInterface();
			GUI_textBox_searchbox.Visible = false;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// La sélection de la liste de droite a changé.
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre par défaut.</param>

		private void GUI_listBox_liste_de_droite_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (GUI_listBox_liste_de_droite.SelectedIndex != -1)
			{
				GUI_textBox_nom_chemin_fichier.Text = liste_fichiers_finale[GUI_listBox_liste_de_droite.SelectedIndex].chemin_complet;
				GUI_textBox_pourcentage.Text = "0";
				GUI_textBox_champ_priorité.Text = "1";
			}
			else
			{
				GUI_textBox_nom_chemin_fichier.Text = "";
				GUI_textBox_pourcentage.Text = "0";
				GUI_textBox_champ_priorité.Text = "0";
			}
		}

		#endregion Sélection dans une liste changée

		#region Quitter le programme

		/// <summary>
		/// Tentative de quitter l'application (par Fichier/Quitter,la croix en
		/// haut à droite,Alt+F4 ou autre chose)
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Argument contenant un élément qui permet d'annuler
		/// la fermeture de l'application.</param>

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{

			e.Cancel=true;

			DialogResult choix = MessageBox.Show("Avez-vous enregistré tout ce que vous deviez?", "Quitter", MessageBoxButtons.YesNo);

			if (choix == DialogResult.Yes)
			{

				e.Cancel = false;

				if (liste_fichiers != null)
					liste_fichiers.Clear();
				if (liste_fichiers_finale != null)
					liste_fichiers_finale.Clear();
				if (liste_fichiers_priotri != null)
					liste_fichiers_priotri.Clear();
				if (liste_saisie_semiauto != null)
					liste_saisie_semiauto.Clear();

			}

		}

		#endregion Quitter le programme

		//---------------------------------------------------------------//
		//---------------------------------------------------------------//

		#region Fonction OuvrirPlaylist

		/// <summary>
		/// Ouvre et lit le contenu d'une playlist afin
		///  de générer la liste de gauche.
		/// </summary>
		/// <param name="strNomFichier">Nom du fichier de playlist ouvert</param>

		void OuvrirPlaylist(string strNomFichier)
		{

			//Réinitialise la liste de gauche
			if (liste_fichiers != null) liste_fichiers.Clear();
			if (liste_fichiers_priotri != null) liste_fichiers_priotri.Clear();
			if (liste_saisie_semiauto != null) liste_saisie_semiauto.Clear();
			liste_fichiers = new List<Fichier>();
			liste_fichiers_priotri = new List<Fichier>();
			liste_saisie_semiauto = new AutoCompleteStringCollection();

			//Réinitialise la liste de droite
			if (liste_fichiers_finale != null)
			{
				liste_fichiers_finale.Clear();
				liste_fichiers_finale = null;
			}

			//Réinitialise les variables
			bListeGénérée = false;
			nPrioritéTotale = 0;

			//Réinitialise les listes de l'interface graphique
			GUI_listBox_liste_de_droite.Items.Clear();
			GUI_listBox_liste_de_gauche.Items.Clear();
			GUI_checkBox_tri_par_priorité.CheckState = CheckState.Unchecked;

			if (strNomFichier != null && File.Exists(strNomFichier))
			{

				StreamReader fichier_lu = new StreamReader(strNomFichier,System.Text.Encoding.Default);
				string ligne_lue;

				try
				{
					ligne_lue = fichier_lu.ReadLine();
					while (ligne_lue != null)
					{
						AjouteFichier(ligne_lue);
						ligne_lue = fichier_lu.ReadLine();
					}
				}
				catch (EndOfStreamException) { /*fin de fichier atteinte*/}
				finally
				{
					fichier_lu.Close();
				}

				playlist_courante = strNomFichier;

			}

			MajInterface();

		}

		#endregion Fonction OuvrirPlaylist

		#region Fonction AjouteFichier

		/// <summary>
		/// Ajoute un fichier à la liste,en gérant la liste en mémoire,
		/// la liste affichée et une partie des variables liées aux listes.
		/// </summary>
		/// <param name="strNomFichier">Fichier à ajouter à la liste.</param>
		/// <returns>Index auquel a été ajouté le fichier,ou index auquel il
		/// existait déjà.</returns>

		int AjouteFichier(string strNomFichier)
		{
			return AjouteFichier(strNomFichier, -1);
		}

		/// <summary>
		/// Ajoute un fichier à la liste,en gérant la liste en mémoire,
		/// la liste affichée et une partie des variables liées aux listes.
		/// Permet de définir manuellement une priorité (message d'erreur si
		/// une priorité existait alors déjà).
		/// </summary>
		/// <param name="strNomFichier">Fichier à ajouter à la liste.</param>
		/// <param name="nPriorité_voulue">Priorité du fichier ajouté.-1 si non utilisé.</param>
		/// <returns>Index auquel a été ajouté le fichier,ou index auquel il
		/// existait déjà si nPriorité_voulue == -1.</returns>

		int AjouteFichier(string strNomFichier,int nPriorité_voulue)
		{
			
			Fichier nom_lu = new Fichier(strNomFichier);
			bool bFound = false;		//trouvé un point d'arrêt dans la recherche
			int i = 0;

			#region Ajout aux listes triées

			//recherche du fichier déjà existant ou du premier fichier supérieur
			while (!bFound && i < liste_fichiers.Count)
			{
				if (
					#region liste_fichiers[i].nom<nom_lu.nom
					string.Compare(liste_fichiers[i].nom,nom_lu.nom,true)<0
					#endregion liste_fichiers[i].nom<nom_lu.nom
				)
					i++;
				else
					bFound = true;
			}

			if (bFound)
			{
				if (
				#region liste_fichiers[i].nom==nom_lu.nom
string.Compare(liste_fichiers[i].nom, nom_lu.nom, true) == 0
				#endregion liste_fichiers[i].nom==nom_lu.nom
				)
				{

					if (
					#region liste_fichiers[i].chemin_complet==nom_lu.chemin_complet
string.Compare(liste_fichiers[i].chemin_complet, nom_lu.chemin_complet, true) == 0
					#endregion liste_fichiers[i].chemin_complet==nom_lu.chemin_complet
					)
					{

						if (nPriorité_voulue == -1)
						{

							int j;
							int nIndexTriPriorite = liste_fichiers_priotri.IndexOf(liste_fichiers[i]);
							Fichier fichier_pour_TriPriorite = liste_fichiers_priotri[nIndexTriPriorite];
							bool bTriPrioriteFait = false;

							liste_fichiers[i].priorite = liste_fichiers[i].priorite + 1;

							liste_fichiers_priotri.RemoveAt(nIndexTriPriorite);

							for (j = 0; j < liste_fichiers_priotri.Count && !bTriPrioriteFait; j++)
								if (liste_fichiers_priotri[j].priorite > liste_fichiers[i].priorite)
									bTriPrioriteFait = true;

							liste_fichiers_priotri.Insert(j, fichier_pour_TriPriorite);
							liste_fichiers_priotri[j].priorite = liste_fichiers[i].priorite;
							

						}
						else
							MessageBox.Show("Usage incorrect de la fonction AjouteFichier", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

					}
					else
					{

						while (
							#region liste_fichiers[i].nom==nom_lu.nom
							string.Compare(liste_fichiers[i].nom, nom_lu.nom, true) == 0
							#endregion liste_fichiers[i].nom==nom_lu.nom
							&&
							#region liste_fichiers[i].chemin_complet!=nom_lu.chemin_complet
							string.Compare(liste_fichiers[i].chemin_complet, nom_lu.chemin_complet, true) != 0
							#endregion liste_fichiers[i].chemin_complet!=nom_lu.chemin_complet
						)
							i++;

						if (
							#region liste_fichiers[i].nom==nom_lu.nom
							string.Compare(liste_fichiers[i].nom, nom_lu.nom, true) == 0
							#endregion liste_fichiers[i].nom==nom_lu.nom
						)
						{

							if (nPriorité_voulue == -1)
							{

								int j;
								int nIndexTriPriorite = liste_fichiers_priotri.IndexOf(liste_fichiers[i]);
								Fichier fichier_pour_TriPriorite = liste_fichiers_priotri[nIndexTriPriorite];
								bool bTriPrioriteFait = false;

								liste_fichiers[i].priorite = liste_fichiers[i].priorite + 1;

								liste_fichiers_priotri.RemoveAt(nIndexTriPriorite);

								for (j = 0; j < liste_fichiers_priotri.Count && !bTriPrioriteFait; j++)
									if (liste_fichiers_priotri[j].priorite > liste_fichiers[i].priorite)
										bTriPrioriteFait = true;

								liste_fichiers_priotri.Insert(j, fichier_pour_TriPriorite);
								liste_fichiers_priotri[j].priorite = liste_fichiers[i].priorite;

							}
							else
								MessageBox.Show("Usage incorrect de la fonction AjouteFichier", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

						}
						else
						{

							int j = 0;

							liste_fichiers.Insert(i, nom_lu);

							if (nPriorité_voulue != -1)
								liste_fichiers[i].priorite = nPriorité_voulue;

							liste_saisie_semiauto.Insert(i, nom_lu.nom);

							if (nPriorité_voulue == -1)
								while (j < liste_fichiers_priotri.Count && liste_fichiers_priotri[j].priorite > 1)
									j++;
							else
								while (j < liste_fichiers_priotri.Count && liste_fichiers_priotri[j].priorite > nPriorité_voulue)
									j++;

							liste_fichiers_priotri.Insert(j, nom_lu);

							if (nPriorité_voulue != -1)
								liste_fichiers_priotri[j].priorite = nPriorité_voulue;

							if (GUI_checkBox_tri_par_priorité.CheckState == CheckState.Checked)
								GUI_listBox_liste_de_gauche.Items.Insert(j, nom_lu.nom);
							else
								GUI_listBox_liste_de_gauche.Items.Insert(i, nom_lu.nom);

							if (GUI_checkBox_tri_par_priorité.CheckState == CheckState.Checked)
								i = j;

						}

					}
				}
				else
				{

					int j = 0;

					liste_fichiers.Insert(i, nom_lu);

					if (nPriorité_voulue != -1)
						liste_fichiers[i].priorite = nPriorité_voulue;

					liste_saisie_semiauto.Insert(i, nom_lu.nom);

					if (nPriorité_voulue == -1)
						while (j < liste_fichiers_priotri.Count && liste_fichiers_priotri[j].priorite > 1)
							j++;
					else
						while (j < liste_fichiers_priotri.Count && liste_fichiers_priotri[j].priorite > nPriorité_voulue)
							j++;

					liste_fichiers_priotri.Insert(j, nom_lu);

					if (nPriorité_voulue != -1)
						liste_fichiers_priotri[j].priorite = nPriorité_voulue;

					if (GUI_checkBox_tri_par_priorité.CheckState == CheckState.Checked)
						GUI_listBox_liste_de_gauche.Items.Insert(j, nom_lu.nom);
					else
						GUI_listBox_liste_de_gauche.Items.Insert(i, nom_lu.nom);

					if (GUI_checkBox_tri_par_priorité.CheckState == CheckState.Checked)
						i = j;

				}
			}
			else
			{

				int j = 0;

				liste_fichiers.Insert(i, nom_lu);

				if (nPriorité_voulue != -1)
					liste_fichiers[i].priorite = nPriorité_voulue;

				liste_saisie_semiauto.Insert(i, nom_lu.nom);

				if (nPriorité_voulue == -1)
					while (j < liste_fichiers_priotri.Count && liste_fichiers_priotri[j].priorite > 1)
						j++;
				else
					while (j < liste_fichiers_priotri.Count && liste_fichiers_priotri[j].priorite > nPriorité_voulue)
						j++;

				liste_fichiers_priotri.Insert(j, nom_lu);

				if (nPriorité_voulue != -1)
					liste_fichiers_priotri[j].priorite = nPriorité_voulue;

				if (GUI_checkBox_tri_par_priorité.CheckState == CheckState.Checked)
					GUI_listBox_liste_de_gauche.Items.Insert(j, nom_lu.nom);
				else
					GUI_listBox_liste_de_gauche.Items.Insert(i, nom_lu.nom);

				if (GUI_checkBox_tri_par_priorité.CheckState == CheckState.Checked)
					i = j;

			}

			#endregion ajout à la liste triée par nom

			if (nPriorité_voulue == -1)
				nPrioritéTotale++;
			else
				nPrioritéTotale += nPriorité_voulue;

			return i;

		}

		#endregion Fonction AjouteFichier

		#region MajInterface

		/// <summary>
		/// Met à jour les valeurs et textes de l'interface graphique.
		/// </summary>

		void MajInterface()
		{

			//Met à jour l'affichage des variables globales
			GUI_textBox_nombre_fichiers.Text = GUI_listBox_liste_de_gauche.Items.Count.ToString();
			GUI_textBox_priorité_totale.Text = nPrioritéTotale.ToString();

			//Met à jour les variables liées sélection courante
			int nIndex = GUI_listBox_liste_de_gauche.SelectedIndex;
			if (nIndex != -1)
			{

				if (GUI_checkBox_tri_par_priorité.CheckState == CheckState.Checked)
				{
					GUI_textBox_champ_priorité.Text = liste_fichiers_priotri[nIndex].priorite.ToString();
					if (nPrioritéTotale != 0)
						GUI_textBox_pourcentage.Text = ((100 * liste_fichiers_priotri[nIndex].priorite) / nPrioritéTotale).ToString();
					else
						GUI_textBox_pourcentage.Text = "0";
					GUI_textBox_nom_chemin_fichier.Text = liste_fichiers_priotri[nIndex].chemin_complet;
				}
				else
				{
					GUI_textBox_champ_priorité.Text = liste_fichiers[nIndex].priorite.ToString();
					if (nPrioritéTotale != 0)
						GUI_textBox_pourcentage.Text = ((100 * liste_fichiers[nIndex].priorite) / nPrioritéTotale).ToString();
					else
						GUI_textBox_pourcentage.Text = "0";
					GUI_textBox_nom_chemin_fichier.Text = liste_fichiers[nIndex].chemin_complet;
				}

			}
			else
			{
				GUI_textBox_champ_priorité.Text = "";
				GUI_textBox_pourcentage.Text = "";
				GUI_textBox_nom_chemin_fichier.Text = "";
			}

		}

		#endregion MajInterface

		//---------------------------------------------------------------//
		//---------------------------------------------------------------//

	}

}

//Notes supplémentaire
/*
A propos de:
x=(valeur*priorite_totale-100*liste_fichiers[index].priorite)/(100-valeur);

on a:	(liste_fichiers[index].priorite+x)/(priorite_totale+x)==valeur/100
	=>	(liste_fichiers[index].priorite+x)==((valeur*priorite_totale)+(valeur*x))/100			=> le cas x==priorite_totale est géré séparément (cas du 100%) donc OK
	=>	(100*liste_fichiers[index].priorite)+(100*x)==(valeur*priorite_totale)+(valeur*x)
	=>	(100-valeur)*x==(valeur*priorite_totale)-(100*liste_fichiers[index].priorite)
	=>	x==(valeur*priorite_totale-100*liste_fichiers[index].priorite)/(100-valeur)			=> de nouveau,le cas du 100% est géré séparément donc ok
*/