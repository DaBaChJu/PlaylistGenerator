namespace Playlist_Generator_v2
{
	partial class Form1
	{
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.GUI_menu_principal = new System.Windows.Forms.MenuStrip();
			this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nouveauToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ouvrirUnePlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ajouterDesVidéosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enregistrerSousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.aProposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.GUI_menu_contextuel = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ajouterDesVidéosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.ajouterUnePlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.GUI_listBox_liste_de_gauche = new System.Windows.Forms.ListBox();
			this.GUI_listBox_liste_de_droite = new System.Windows.Forms.ListBox();
			this.GUI_textBox_nom_chemin_fichier = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.GUI_textBox_champ_priorité = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.GUI_textBox_pourcentage = new System.Windows.Forms.TextBox();
			this.GUI_button_bouton_valider = new System.Windows.Forms.Button();
			this.GUI_checkBox_tri_par_priorité = new System.Windows.Forms.CheckBox();
			this.GUI_button_bouton_générer = new System.Windows.Forms.Button();
			this.GUI_button_bouton_random = new System.Windows.Forms.Button();
			this.GUI_button_bouton_haut = new System.Windows.Forms.Button();
			this.GUI_button_bouton_bas = new System.Windows.Forms.Button();
			this.GUI_textBox_nombre_fichiers = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.GUI_textBox_priorité_totale = new System.Windows.Forms.TextBox();
			this.GUI_button_plus = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.GUI_button_bouton_éditer = new System.Windows.Forms.Button();
			this.GUI_textBox_searchbox = new System.Windows.Forms.TextBox();
			this.GUI_menu_principal.SuspendLayout();
			this.GUI_menu_contextuel.SuspendLayout();
			this.SuspendLayout();
			// 
			// GUI_menu_principal
			// 
			this.GUI_menu_principal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.toolStripMenuItem1});
			this.GUI_menu_principal.Location = new System.Drawing.Point(0, 0);
			this.GUI_menu_principal.Name = "GUI_menu_principal";
			this.GUI_menu_principal.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
			this.GUI_menu_principal.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.GUI_menu_principal.Size = new System.Drawing.Size(910, 24);
			this.GUI_menu_principal.TabIndex = 0;
			this.GUI_menu_principal.Text = "menuStrip1";
			// 
			// fichierToolStripMenuItem
			// 
			this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nouveauToolStripMenuItem,
            this.ouvrirUnePlaylistToolStripMenuItem,
            this.ajouterDesVidéosToolStripMenuItem,
            this.enregistrerSousToolStripMenuItem,
            this.quitterToolStripMenuItem});
			this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
			this.fichierToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
			this.fichierToolStripMenuItem.Text = "Fichier";
			// 
			// nouveauToolStripMenuItem
			// 
			this.nouveauToolStripMenuItem.Name = "nouveauToolStripMenuItem";
			this.nouveauToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.nouveauToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.nouveauToolStripMenuItem.Text = "&Nouveau";
			this.nouveauToolStripMenuItem.Click += new System.EventHandler(this.nouveauToolStripMenuItem_Click);
			// 
			// ouvrirUnePlaylistToolStripMenuItem
			// 
			this.ouvrirUnePlaylistToolStripMenuItem.Name = "ouvrirUnePlaylistToolStripMenuItem";
			this.ouvrirUnePlaylistToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.ouvrirUnePlaylistToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.ouvrirUnePlaylistToolStripMenuItem.Text = "&Ouvrir une playlist...";
			this.ouvrirUnePlaylistToolStripMenuItem.Click += new System.EventHandler(this.ouvrirUnePlaylistToolStripMenuItem_Click);
			// 
			// ajouterDesVidéosToolStripMenuItem
			// 
			this.ajouterDesVidéosToolStripMenuItem.Name = "ajouterDesVidéosToolStripMenuItem";
			this.ajouterDesVidéosToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.ajouterDesVidéosToolStripMenuItem.Text = "&Ajouter des vidéos...";
			this.ajouterDesVidéosToolStripMenuItem.Click += new System.EventHandler(this.ajouterDesVidéosToolStripMenuItem_Click);
			// 
			// enregistrerSousToolStripMenuItem
			// 
			this.enregistrerSousToolStripMenuItem.Name = "enregistrerSousToolStripMenuItem";
			this.enregistrerSousToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.enregistrerSousToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.enregistrerSousToolStripMenuItem.Text = "Enregistrer &Sous...";
			this.enregistrerSousToolStripMenuItem.Click += new System.EventHandler(this.enregistrerSousToolStripMenuItem_Click);
			// 
			// quitterToolStripMenuItem
			// 
			this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
			this.quitterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.quitterToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.quitterToolStripMenuItem.Text = "&Quitter";
			this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aProposToolStripMenuItem});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
			this.toolStripMenuItem1.Text = "?";
			// 
			// aProposToolStripMenuItem
			// 
			this.aProposToolStripMenuItem.Name = "aProposToolStripMenuItem";
			this.aProposToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.aProposToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.aProposToolStripMenuItem.Text = "A propos";
			this.aProposToolStripMenuItem.Click += new System.EventHandler(this.aProposToolStripMenuItem_Click);
			// 
			// GUI_menu_contextuel
			// 
			this.GUI_menu_contextuel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterDesVidéosToolStripMenuItem1,
            this.ajouterUnePlaylistToolStripMenuItem});
			this.GUI_menu_contextuel.Name = "GUI_menu_contextuel";
			this.GUI_menu_contextuel.Size = new System.Drawing.Size(180, 48);
			// 
			// ajouterDesVidéosToolStripMenuItem1
			// 
			this.ajouterDesVidéosToolStripMenuItem1.Name = "ajouterDesVidéosToolStripMenuItem1";
			this.ajouterDesVidéosToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
			this.ajouterDesVidéosToolStripMenuItem1.Text = "Ajouter des vidéos...";
			this.ajouterDesVidéosToolStripMenuItem1.Click += new System.EventHandler(this.ajouterDesVidéosToolStripMenuItem_Click);
			// 
			// ajouterUnePlaylistToolStripMenuItem
			// 
			this.ajouterUnePlaylistToolStripMenuItem.Name = "ajouterUnePlaylistToolStripMenuItem";
			this.ajouterUnePlaylistToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.ajouterUnePlaylistToolStripMenuItem.Text = "Ajouter une playlist...";
			// 
			// GUI_listBox_liste_de_gauche
			// 
			this.GUI_listBox_liste_de_gauche.AllowDrop = true;
			this.GUI_listBox_liste_de_gauche.ContextMenuStrip = this.GUI_menu_contextuel;
			this.GUI_listBox_liste_de_gauche.FormattingEnabled = true;
			this.GUI_listBox_liste_de_gauche.Location = new System.Drawing.Point(9, 26);
			this.GUI_listBox_liste_de_gauche.Name = "GUI_listBox_liste_de_gauche";
			this.GUI_listBox_liste_de_gauche.Size = new System.Drawing.Size(222, 433);
			this.GUI_listBox_liste_de_gauche.TabIndex = 1;
			this.GUI_listBox_liste_de_gauche.SelectedIndexChanged += new System.EventHandler(this.GUI_listBox_liste_de_gauche_SelectedIndexChanged);
			this.GUI_listBox_liste_de_gauche.Leave += new System.EventHandler(this.GUI_listBox_liste_de_gauche_Leave);
			this.GUI_listBox_liste_de_gauche.DragDrop += new System.Windows.Forms.DragEventHandler(this.GUI_listBox_liste_de_gauche_DragDrop);
			this.GUI_listBox_liste_de_gauche.DragEnter += new System.Windows.Forms.DragEventHandler(this.GUI_DragEnter);
			this.GUI_listBox_liste_de_gauche.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GUI_listBox_liste_de_gauche_KeyPress);
			this.GUI_listBox_liste_de_gauche.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GUI_listBox_liste_de_gauche_KeyUp);
			// 
			// GUI_listBox_liste_de_droite
			// 
			this.GUI_listBox_liste_de_droite.FormattingEnabled = true;
			this.GUI_listBox_liste_de_droite.Location = new System.Drawing.Point(680, 26);
			this.GUI_listBox_liste_de_droite.Name = "GUI_listBox_liste_de_droite";
			this.GUI_listBox_liste_de_droite.Size = new System.Drawing.Size(222, 433);
			this.GUI_listBox_liste_de_droite.TabIndex = 18;
			this.GUI_listBox_liste_de_droite.SelectedIndexChanged += new System.EventHandler(this.GUI_listBox_liste_de_droite_SelectedIndexChanged);
			this.GUI_listBox_liste_de_droite.Leave += new System.EventHandler(this.GUI_listBox_liste_de_droite_Leave);
			// 
			// GUI_textBox_nom_chemin_fichier
			// 
			this.GUI_textBox_nom_chemin_fichier.Location = new System.Drawing.Point(237, 26);
			this.GUI_textBox_nom_chemin_fichier.Name = "GUI_textBox_nom_chemin_fichier";
			this.GUI_textBox_nom_chemin_fichier.ReadOnly = true;
			this.GUI_textBox_nom_chemin_fichier.Size = new System.Drawing.Size(397, 20);
			this.GUI_textBox_nom_chemin_fichier.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(336, 67);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Priorité:";
			// 
			// GUI_textBox_champ_priorité
			// 
			this.GUI_textBox_champ_priorité.Location = new System.Drawing.Point(384, 64);
			this.GUI_textBox_champ_priorité.MaxLength = 5;
			this.GUI_textBox_champ_priorité.Name = "GUI_textBox_champ_priorité";
			this.GUI_textBox_champ_priorité.Size = new System.Drawing.Size(39, 20);
			this.GUI_textBox_champ_priorité.TabIndex = 5;
			this.GUI_textBox_champ_priorité.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(465, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Pourcentage:";
			// 
			// GUI_textBox_pourcentage
			// 
			this.GUI_textBox_pourcentage.Location = new System.Drawing.Point(542, 64);
			this.GUI_textBox_pourcentage.MaxLength = 3;
			this.GUI_textBox_pourcentage.Name = "GUI_textBox_pourcentage";
			this.GUI_textBox_pourcentage.Size = new System.Drawing.Size(33, 20);
			this.GUI_textBox_pourcentage.TabIndex = 9;
			this.GUI_textBox_pourcentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// GUI_button_bouton_valider
			// 
			this.GUI_button_bouton_valider.Location = new System.Drawing.Point(418, 98);
			this.GUI_button_bouton_valider.Name = "GUI_button_bouton_valider";
			this.GUI_button_bouton_valider.Size = new System.Drawing.Size(75, 23);
			this.GUI_button_bouton_valider.TabIndex = 10;
			this.GUI_button_bouton_valider.Text = "Valider";
			this.GUI_button_bouton_valider.UseVisualStyleBackColor = true;
			this.GUI_button_bouton_valider.Click += new System.EventHandler(this.GUI_button_bouton_valider_Click);
			// 
			// GUI_checkBox_tri_par_priorité
			// 
			this.GUI_checkBox_tri_par_priorité.AutoSize = true;
			this.GUI_checkBox_tri_par_priorité.Location = new System.Drawing.Point(406, 133);
			this.GUI_checkBox_tri_par_priorité.Name = "GUI_checkBox_tri_par_priorité";
			this.GUI_checkBox_tri_par_priorité.Size = new System.Drawing.Size(99, 17);
			this.GUI_checkBox_tri_par_priorité.TabIndex = 11;
			this.GUI_checkBox_tri_par_priorité.Text = "Trier par priorité";
			this.GUI_checkBox_tri_par_priorité.UseVisualStyleBackColor = true;
			this.GUI_checkBox_tri_par_priorité.CheckedChanged += new System.EventHandler(this.GUI_checkBox_tri_par_priorité_CheckedChanged);
			// 
			// GUI_button_bouton_générer
			// 
			this.GUI_button_bouton_générer.Location = new System.Drawing.Point(418, 192);
			this.GUI_button_bouton_générer.Name = "GUI_button_bouton_générer";
			this.GUI_button_bouton_générer.Size = new System.Drawing.Size(75, 75);
			this.GUI_button_bouton_générer.TabIndex = 12;
			this.GUI_button_bouton_générer.Text = "Générer liste";
			this.GUI_button_bouton_générer.UseVisualStyleBackColor = true;
			this.GUI_button_bouton_générer.Click += new System.EventHandler(this.GUI_button_bouton_générer_Click);
			// 
			// GUI_button_bouton_random
			// 
			this.GUI_button_bouton_random.AutoSize = true;
			this.GUI_button_bouton_random.Location = new System.Drawing.Point(392, 273);
			this.GUI_button_bouton_random.Name = "GUI_button_bouton_random";
			this.GUI_button_bouton_random.Size = new System.Drawing.Size(127, 48);
			this.GUI_button_bouton_random.TabIndex = 13;
			this.GUI_button_bouton_random.Text = "Randomiser liste";
			this.GUI_button_bouton_random.UseVisualStyleBackColor = true;
			this.GUI_button_bouton_random.Click += new System.EventHandler(this.GUI_button_bouton_random_Click);
			// 
			// GUI_button_bouton_haut
			// 
			this.GUI_button_bouton_haut.AutoSize = true;
			this.GUI_button_bouton_haut.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.GUI_button_bouton_haut.Location = new System.Drawing.Point(645, 203);
			this.GUI_button_bouton_haut.Name = "GUI_button_bouton_haut";
			this.GUI_button_bouton_haut.Size = new System.Drawing.Size(29, 23);
			this.GUI_button_bouton_haut.TabIndex = 19;
			this.GUI_button_bouton_haut.Text = "↑";
			this.GUI_button_bouton_haut.UseVisualStyleBackColor = true;
			this.GUI_button_bouton_haut.Click += new System.EventHandler(this.GUI_button_bouton_haut_Click);
			// 
			// GUI_button_bouton_bas
			// 
			this.GUI_button_bouton_bas.AutoSize = true;
			this.GUI_button_bouton_bas.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.GUI_button_bouton_bas.Location = new System.Drawing.Point(645, 233);
			this.GUI_button_bouton_bas.Name = "GUI_button_bouton_bas";
			this.GUI_button_bouton_bas.Size = new System.Drawing.Size(29, 23);
			this.GUI_button_bouton_bas.TabIndex = 20;
			this.GUI_button_bouton_bas.Text = "↓";
			this.GUI_button_bouton_bas.UseVisualStyleBackColor = true;
			this.GUI_button_bouton_bas.Click += new System.EventHandler(this.GUI_button_bouton_bas_Click);
			// 
			// GUI_textBox_nombre_fichiers
			// 
			this.GUI_textBox_nombre_fichiers.Location = new System.Drawing.Point(406, 436);
			this.GUI_textBox_nombre_fichiers.MaxLength = 6;
			this.GUI_textBox_nombre_fichiers.Name = "GUI_textBox_nombre_fichiers";
			this.GUI_textBox_nombre_fichiers.ReadOnly = true;
			this.GUI_textBox_nombre_fichiers.Size = new System.Drawing.Size(44, 20);
			this.GUI_textBox_nombre_fichiers.TabIndex = 15;
			this.GUI_textBox_nombre_fichiers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(317, 439);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(83, 13);
			this.label3.TabIndex = 14;
			this.label3.Text = "Nombre fichiers:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(466, 439);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(71, 13);
			this.label4.TabIndex = 16;
			this.label4.Text = "Priorité totale:";
			// 
			// GUI_textBox_priorité_totale
			// 
			this.GUI_textBox_priorité_totale.Location = new System.Drawing.Point(543, 436);
			this.GUI_textBox_priorité_totale.Name = "GUI_textBox_priorité_totale";
			this.GUI_textBox_priorité_totale.ReadOnly = true;
			this.GUI_textBox_priorité_totale.Size = new System.Drawing.Size(51, 20);
			this.GUI_textBox_priorité_totale.TabIndex = 17;
			this.GUI_textBox_priorité_totale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// GUI_button_plus
			// 
			this.GUI_button_plus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.GUI_button_plus.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GUI_button_plus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.GUI_button_plus.Location = new System.Drawing.Point(429, 59);
			this.GUI_button_plus.Name = "GUI_button_plus";
			this.GUI_button_plus.Size = new System.Drawing.Size(15, 15);
			this.GUI_button_plus.TabIndex = 6;
			this.GUI_button_plus.Text = "+";
			this.GUI_button_plus.UseVisualStyleBackColor = true;
			this.GUI_button_plus.Click += new System.EventHandler(this.GUI_button_plus_Click);
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(429, 75);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(15, 15);
			this.button1.TabIndex = 7;
			this.button1.Text = "-";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.GUI_button_moins_Click);
			// 
			// GUI_button_bouton_éditer
			// 
			this.GUI_button_bouton_éditer.Location = new System.Drawing.Point(639, 25);
			this.GUI_button_bouton_éditer.Name = "GUI_button_bouton_éditer";
			this.GUI_button_bouton_éditer.Size = new System.Drawing.Size(34, 20);
			this.GUI_button_bouton_éditer.TabIndex = 3;
			this.GUI_button_bouton_éditer.Text = "Edit";
			this.GUI_button_bouton_éditer.UseVisualStyleBackColor = true;
			this.GUI_button_bouton_éditer.Click += new System.EventHandler(this.bouton_éditer_Click);
			// 
			// GUI_textBox_searchbox
			// 
			this.GUI_textBox_searchbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.GUI_textBox_searchbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.GUI_textBox_searchbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.GUI_textBox_searchbox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.GUI_textBox_searchbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.GUI_textBox_searchbox.Location = new System.Drawing.Point(11, 455);
			this.GUI_textBox_searchbox.Name = "GUI_textBox_searchbox";
			this.GUI_textBox_searchbox.Size = new System.Drawing.Size(218, 13);
			this.GUI_textBox_searchbox.TabIndex = 21;
			this.GUI_textBox_searchbox.Visible = false;
			// 
			// Form1
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(910, 469);
			this.Controls.Add(this.GUI_textBox_searchbox);
			this.Controls.Add(this.GUI_button_bouton_éditer);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.GUI_button_plus);
			this.Controls.Add(this.GUI_textBox_priorité_totale);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.GUI_textBox_nombre_fichiers);
			this.Controls.Add(this.GUI_button_bouton_bas);
			this.Controls.Add(this.GUI_button_bouton_haut);
			this.Controls.Add(this.GUI_button_bouton_random);
			this.Controls.Add(this.GUI_button_bouton_générer);
			this.Controls.Add(this.GUI_checkBox_tri_par_priorité);
			this.Controls.Add(this.GUI_button_bouton_valider);
			this.Controls.Add(this.GUI_textBox_pourcentage);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.GUI_textBox_champ_priorité);
			this.Controls.Add(this.GUI_textBox_nom_chemin_fichier);
			this.Controls.Add(this.GUI_listBox_liste_de_droite);
			this.Controls.Add(this.GUI_menu_principal);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.GUI_listBox_liste_de_gauche);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.GUI_menu_principal;
			this.Name = "Form1";
			this.Text = "Générateur de Playlist v2";
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.GUI_DragEnter);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.GUI_menu_principal.ResumeLayout(false);
			this.GUI_menu_principal.PerformLayout();
			this.GUI_menu_contextuel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip GUI_menu_principal;
		private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem nouveauToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ouvrirUnePlaylistToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ajouterDesVidéosToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem enregistrerSousToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aProposToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip GUI_menu_contextuel;
		private System.Windows.Forms.ToolStripMenuItem ajouterDesVidéosToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem ajouterUnePlaylistToolStripMenuItem;
		private System.Windows.Forms.ListBox GUI_listBox_liste_de_gauche;
		private System.Windows.Forms.ListBox GUI_listBox_liste_de_droite;
		private System.Windows.Forms.TextBox GUI_textBox_nom_chemin_fichier;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox GUI_textBox_champ_priorité;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox GUI_textBox_pourcentage;
		private System.Windows.Forms.Button GUI_button_bouton_valider;
		private System.Windows.Forms.CheckBox GUI_checkBox_tri_par_priorité;
		private System.Windows.Forms.Button GUI_button_bouton_générer;
		private System.Windows.Forms.Button GUI_button_bouton_random;
		private System.Windows.Forms.Button GUI_button_bouton_haut;
		private System.Windows.Forms.Button GUI_button_bouton_bas;
		private System.Windows.Forms.TextBox GUI_textBox_nombre_fichiers;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox GUI_textBox_priorité_totale;
		private System.Windows.Forms.Button GUI_button_plus;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button GUI_button_bouton_éditer;
		private System.Windows.Forms.TextBox GUI_textBox_searchbox;
	}
}

