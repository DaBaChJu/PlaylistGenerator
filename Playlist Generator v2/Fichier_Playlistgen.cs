namespace Playlist_Generator_v2
{
	public class Fichier
	{

		public string chemin_complet;
		public string nom;
		public int priorite;

		#region Constructeurs

		/// <summary>
		/// Constructeur par défaut
		/// </summary>
		public Fichier()
		{
			chemin_complet = "";
			nom = "";
			priorite = 0;
		}

		/// <summary>
		/// Constructeur à partir d'un chemin de fichier
		/// </summary>
		/// <param name="chemin">Chemin complet du fichier</param>
		public Fichier(string chemin)
		{
			chemin_complet = chemin;
			nom = System.IO.Path.GetFileName(chemin);
			priorite = 1;
		}

		/// <summary>
		/// Constructeur par copie
		/// </summary>
		/// <param name="autre_fichier">Autre objet de la classe Fichier</param>
		public Fichier(Fichier autre_fichier)
		{
			chemin_complet = autre_fichier.chemin_complet;
			nom = autre_fichier.nom;
			priorite = autre_fichier.priorite;
		}

		#endregion Constructeurs

		/*
		#region Surcharges de Equals et GetHashCode demandées par des warnings

		/// <summary>
		/// Surcharge de Object.Equals demandée par le compilateur
		/// </summary>
		/// <param name="o">Objet de type Fichier ou String,sinon retourne false.</param>
		/// <returns>true si le Fichier courant est considéré égal à l'objet o,sinon false.</returns>

		public override bool Equals(object o)
		{

			bool bResult = false;

			if (o is Fichier)
				bResult = (nom == (o as Fichier).nom);
			else
			if (o is string)
				bResult = (nom == (o as string));

			return bResult;

		}

		/// <summary>
		/// Surcharge de Object.GetHashCode demandée par le compilateur
		/// </summary>
		/// <returns>Retourne le code de hachage du membre nom (pour que des comparaisons d'égalité soit cohérentes).</returns>

		public override int GetHashCode()
		{
			return nom.GetHashCode();
		}

		#endregion Surcharges de Equals et GetHashCode demandées par des warnings

		#region Surcharges des opérateurs ==,!=,<= et >=

		public static bool operator ==(Fichier ce_fichier, Fichier autre_fichier)
		{
			return ce_fichier.nom == autre_fichier.nom;
		}

		public static bool operator !=(Fichier ce_fichier, Fichier autre_fichier)
		{
			return ce_fichier.nom != autre_fichier.nom;
		}

		public static bool operator ==(Fichier ce_fichier, string nom_fichier)
		{
			return ce_fichier.nom == nom_fichier;
		}

		public static bool operator !=(Fichier ce_fichier, string nom_fichier)
		{
			return ce_fichier.nom != nom_fichier;
		}

		public static bool operator <(Fichier ce_fichier, Fichier autre_fichier)
		{
			return ce_fichier.nom < autre_fichier.nom;
		}

		public static bool operator >(Fichier ce_fichier, Fichier autre_fichier)
		{
			return ce_fichier.nom > autre_fichier.nom;
		}

		public static bool operator <(Fichier ce_fichier, string nom_fichier)
		{
			return ce_fichier.nom < nom_fichier;
		}

		public static bool operator >(Fichier ce_fichier, string nom_fichier)
		{
			return ce_fichier.nom > nom_fichier;
		}

		#endregion Surcharges des opérateurs ==,!=,<= et >=
		*/
	}
}
