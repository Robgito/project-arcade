using System.ComponentModel;
using System.Text.Json.Serialization;


namespace Project_3___Arcade
{
    public partial class Gebruiker : IDataErrorInfo, INotifyPropertyChanged
    {
        private string _gebruikersnaamValidation;
        private string _paswoordValidation;
        private string _emailValidation;

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string GebruikersnaamValidation
        {
            get { return _gebruikersnaamValidation; }
            set
            {
                _gebruikersnaamValidation = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("GebruikersnaamValidation"));
                }
            }
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string EmailValidation
        {
            get { return _emailValidation; }
            set
            {
                _emailValidation = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("EmailValidation"));
                }
            }
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string PaswoordValidation
        {
            get { return _paswoordValidation; }
            set
            {
                _paswoordValidation = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PaswoordValidation"));
                }
            }
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string result = null;

                if (columnName == "GebruikersnaamValidation")
                {
                    if (string.IsNullOrEmpty(GebruikersnaamValidation))
                        result = "Verplicht veld";
                }
                if (columnName == "PaswoordValidation")
                {
                    if (string.IsNullOrEmpty(PaswoordValidation))
                        result = "Verplicht veld";
                }
                if (columnName == "EmailValidation")
                {
                    if (string.IsNullOrEmpty(EmailValidation))
                        result = "Verplicht veld";
                }
                if (columnName == "Gebruikersnaam")
                {
                    if (string.IsNullOrEmpty(Gebruikersnaam))
                        result = "Verplicht veld";
                }
                if (columnName == "Paswoord")
                {
                    if (string.IsNullOrEmpty(Paswoord))
                        result = "Verplicht veld";
                }
                if (columnName == "Email")
                {
                    if (string.IsNullOrEmpty(Email))
                        result = "Verplicht veld";
                }
                return result;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"UserID:{UserID}; Naam: {Gebruikersnaam}; Wachtwoord: {PaswoordEncreptie()};";
        }

        public string PaswoordEncreptie()
        {

            return new string('*', Paswoord.Length);
        }
        public override bool Equals(object obj)
        {
            bool resultaat = false;
            if (obj != null)
            {
                if (GetType() == obj.GetType())
                {
                    Gebruiker r = (Gebruiker)obj;
                    if (this.Gebruikersnaam == r.Gebruikersnaam || this.Email == r.Email)
                    {
                        resultaat = true;
                    }
                }
            }
            return resultaat;
        }
    }
}
