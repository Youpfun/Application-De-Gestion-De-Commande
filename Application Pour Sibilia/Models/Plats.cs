using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Application_Pour_Sibilia.Models
{
    public class Plat
    {
        private int numPlat;
        private int numSousCategorie;
        private string nomSousCategorie;
        private int numPeriode;
        private string nomPeriode;
        private string nomPlat;
        private decimal prixUnitaire;
        private int delaiPreparation;
        private int nbPersonnes;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Plat(int numPlat)
        {
            this.NumPlat = numPlat;
        }
        public Plat()
        {
        }

        public Plat(int numSousCategorie, int numPeriode, string nomPlat, decimal prixUnitaire, int delaiPreparation, int nbPersonnes)
        {
            this.NumSousCategorie = numSousCategorie;
            this.NumPeriode = numPeriode;
            this.NomPlat = nomPlat;
            this.PrixUnitaire = prixUnitaire;
            this.DelaiPreparation = delaiPreparation;
            this.NbPersonnes = nbPersonnes;
        }
        public Plat(string nomSousCategorie, string nomPeriode, string nomPlat, decimal prixUnitaire, int delaiPreparation, int nbPersonnes)
        {
            this.NomSousCategorie = nomSousCategorie;
            this.NomPeriode = nomPeriode;
            this.NomPlat = nomPlat;
            this.PrixUnitaire = prixUnitaire;
            this.DelaiPreparation = delaiPreparation;
            this.NbPersonnes = nbPersonnes;
        }
        public Plat(int numSousCategorie, int numPeriode, string nomPlat, decimal prixUnitaire, int delaiPreparation, int nbPersonnes, int numPlat)
            : this(numSousCategorie, numPeriode, nomPlat, prixUnitaire, delaiPreparation, nbPersonnes)
        {
            this.NumPlat = numPlat;
        }

        public int NumPlat
        {
            get
            {
                return this.numPlat;
            }
            set
            {
                this.numPlat = value;
            }
        }

        public int NumSousCategorie
        {
            get
            {
                return this.numSousCategorie;
            }
            set
            {
                if (value <= 0) { throw new ArgumentException("Le numéro de sous-catégorie doit être positif"); }
                this.numSousCategorie = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumSousCategorie)));
            }
        }

        public int NumPeriode
        {
            get
            {
                return this.numPeriode;
            }
            set
            {
                if (value <= 0) { throw new ArgumentException("Le numéro de période doit être positif"); }
                this.numPeriode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumPeriode)));
            }
        }
        public string NomPeriode
        {
            get
            {
                return this.nomPeriode;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Le nom de la période doit être renseigné"); }
                if (value.Length > 50) { throw new ArgumentException("Le nom de la période doit avoir moins de 50 caractères"); }
                this.nomPeriode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NomPeriode)));
            }
        }
        public string NomSousCategorie
        {
            get
            {
                return this.nomSousCategorie;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Le nom de la sous-catégorie doit être renseigné"); }
                if (value.Length > 50) { throw new ArgumentException("Le nom de la sous-catégorie doit avoir moins de 50 caractères"); }
                this.nomSousCategorie = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NomSousCategorie)));
            }
        }
        public string NomPlat
        {
            get
            {
                return this.nomPlat;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Le nom du plat doit être renseigné"); }
                if (value.Length > 50) { throw new ArgumentException("Le nom doit avoir moins de 50 caractères"); }
                this.nomPlat = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NomPlat)));
            }
        }

        public decimal PrixUnitaire
        {
            get
            {
                return this.prixUnitaire;
            }
            set
            {
                if (value < 0) { throw new ArgumentException("Le prix unitaire ne peut pas être négatif"); }
                if (value > 999.99m) { throw new ArgumentException("Le prix unitaire ne peut pas dépasser 999.99"); }
                this.prixUnitaire = Math.Round(value, 2);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrixUnitaire)));
            }
        }

        public int DelaiPreparation
        {
            get
            {
                return this.delaiPreparation;
            }
            set
            {
                if (value < 0) { throw new ArgumentException("Le délai de préparation ne peut pas être négatif"); }
                this.delaiPreparation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DelaiPreparation)));
            }
        }

        public int NbPersonnes
        {
            get
            {
                return this.nbPersonnes;
            }
            set
            {
                if (value <= 0) { throw new ArgumentException("Le nombre de personnes doit être positif"); }
                this.nbPersonnes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NbPersonnes)));
            }
        }



        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into plat (numsouscategorie, numperiode, nomplat, prixunitaire, delaipreparation, nbpersonnes) values (@numsouscategorie, @numperiode, @nomplat, @prixunitaire, @delaipreparation, @nbpersonnes) RETURNING numplat"))
            {
                cmdInsert.Parameters.AddWithValue("numsouscategorie", this.NumSousCategorie);
                cmdInsert.Parameters.AddWithValue("numperiode", this.NumPeriode);
                cmdInsert.Parameters.AddWithValue("nomplat", this.NomPlat);
                cmdInsert.Parameters.AddWithValue("prixunitaire", this.PrixUnitaire);
                cmdInsert.Parameters.AddWithValue("delaipreparation", this.DelaiPreparation);
                cmdInsert.Parameters.AddWithValue("nbpersonnes", this.NbPersonnes);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.NumPlat = nb;
            return nb;
        }

        public void Read()
        {
            using (var cmdSelect = new NpgsqlCommand("select * from plat where numplat = @numplat;"))
            {
                cmdSelect.Parameters.AddWithValue("numplat", this.NumPlat);

                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                if (dt.Rows.Count > 0)
                {
                    this.NumSousCategorie = Convert.ToInt32(dt.Rows[0]["numsouscategorie"]);
                    this.NumPeriode = Convert.ToInt32(dt.Rows[0]["numperiode"]);
                    this.NomPlat = (String)dt.Rows[0]["nomplat"];
                    this.PrixUnitaire = Convert.ToDecimal(dt.Rows[0]["prixunitaire"]);
                    this.DelaiPreparation = Convert.ToInt32(dt.Rows[0]["delaipreparation"]);
                    this.NbPersonnes = Convert.ToInt32(dt.Rows[0]["nbpersonnes"]);
                }
            }
        }

        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand("update plat set numsouscategorie = @numsouscategorie, numperiode = @numperiode, nomplat = @nomplat, prixunitaire = @prixunitaire, delaipreparation = @delaipreparation, nbpersonnes = @nbpersonnes where numplat = @numplat;"))
            {
                cmdUpdate.Parameters.AddWithValue("numsouscategorie", this.NumSousCategorie);
                cmdUpdate.Parameters.AddWithValue("numperiode", this.NumPeriode);
                cmdUpdate.Parameters.AddWithValue("nomplat", this.NomPlat);
                cmdUpdate.Parameters.AddWithValue("prixunitaire", this.PrixUnitaire);
                cmdUpdate.Parameters.AddWithValue("delaipreparation", this.DelaiPreparation);
                cmdUpdate.Parameters.AddWithValue("nbpersonnes", this.NbPersonnes);
                cmdUpdate.Parameters.AddWithValue("numplat", this.NumPlat);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public List<Plat> FindAll()
        {
            List<Plat> lesPlats = new List<Plat>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT p.numplat, p.numsouscategorie, p.numperiode, p.nomplat, p.prixunitaire, p.delaipreparation, p.nbpersonnes, c.nomsouscategorie, pe.LIBELLEPERIODE FROM plat p JOIN souscategorie c ON p.NUMSOUSCATEGORIE = c.NUMSOUSCATEGORIE JOIN periode pe ON p.numperiode = pe.numperiode"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesPlats.Add(new Plat(
                        Convert.ToInt32(dr["numsouscategorie"]),
                        Convert.ToInt32(dr["numperiode"]),
                        (string)dr["nomplat"],
                        Convert.ToDecimal(dr["prixunitaire"]),
                        Convert.ToInt32(dr["delaipreparation"]),
                        Convert.ToInt32(dr["nbpersonnes"]),
                        Convert.ToInt32(dr["numplat"])
                    )
                    {
                        NomSousCategorie = dr["nomsouscategorie"].ToString()!,
                        NomPeriode = dr["LIBELLEPERIODE"].ToString()!
                    });
                }
            }
            return lesPlats;
        }
    }
}