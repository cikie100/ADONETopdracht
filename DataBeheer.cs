using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ADONETopdracht
{
    public class DataBeheer
    {
        #region Uitleg v oefening

        /*Deze klasse zal de functionaliteit bevatten :
                     -om de gegevens te lezen vanuit het bestand
                     -en te importeren in de databank

            Daarnaast wensen we in deze klasse ook een methode terug te vinden die:
                     -een nieuw adres toevoegt aan de databank.

      Je mag er vanuit gaan dat zowel de straatnaam als de gemeente reeds bestaat.
      Dus enkel 'adres' en 'adreslocatie' moet worden toegevoegd.
       */

        #endregion Uitleg v oefening

        public DataBeheer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        private string connectionString;

        #region gegevens te lezen vanuit het bestand en te importeren in de databank
        private class Tekstbestand
        {
            private string text;

            public string Text
            {
                get { return text; }
                set { text = value; }
            }
            private string melding;
            public string Melding
            {
                get { return melding; }
            }

            private string fileName;

            public string FileName
            {
                get { return fileName; }
                set
                {
                    this.fileName = value;
                }
            }

            public bool Lees()
            {
                // we gaan ervan uit dat er iets misloopt
                bool result = false;
                try
                {
                    // Creëer een instantie van de StreamReader klasse om
                    // een bestand in te lezen.
                    // Het using statement sluit ook de StreamReader.
                    using (StreamReader sr = new StreamReader(this.fileName))
                    {
                        // Lees tot het einde van het bestand
                        this.text = sr.ReadToEnd();
                        this.melding = String.Format(
                            "Het bestand met de naam {0} is ingelezen.",
                            this.fileName);
                        result = true;
                    }
                }
                catch (Exception e)
                {
                    // Melding aan de gebruiker dat iets verkeerd gelopen is.
                    this.melding = $"Kan het bestand met de naam {this.fileName} niet inlezen.\nFoutmelding {e.Message}.";
                }
                return result;
            } 
        }
        public void ReadFromCSVFile()
        {
            try
            {

                Tekstbestand bestand = new Tekstbestand();
                bestand.FileName = @"C:\Users\ciki3\source\repos\ADONETopdracht\Dal\CrabAdr.gml";
                bestand.Lees();
                // string[]  x= bestand.Text.Split(new string[] { "<agiv:CrabAdr>" }, StringSplitOptions.None);
                // string[] lines = File.ReadAllLines(bestand.Text.Split('<agiv:CrabAdr>'));
                // string[] lines = File.ReadAllLines(x.ToString());

                using (StreamReader sr = File.OpenText(bestand.Text))
                {
                    getConnection();
                    String line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        string id_T = "";
                        string straatnaam = "";
                        string huisnummer_T = "";
                        string appartementnummer_T = "";
                        string busnummer_T = "";
                        // Gemeente g = g.NIScode.Equals( straatnaam.NIScode);
                        string huisnummerlabel_T = "";
                        string postcode_T = "";
                        string d1_T = "";
                        string d2_T = "";

                        if (line.Contains("<agiv:CrabAdr>"))
                        {
                            while ((line = sr.ReadLine()) != "<gml:featureMember>")
                            {
                                if (line.Contains("<agiv:ID>"))
                                {
                                    // Keep lines that does not match
                                    id_T = line.Replace("<agiv:ID>", "");
                                    id_T = id_T.Replace("/<agiv:ID>", "");
                                }
                                if (line.Contains("<agiv:STRAATNM>"))
                                {
                                    // Keep lines that does not match
                                    straatnaam = line.Replace("<agiv:STRAATNM>", "");
                                    straatnaam = straatnaam.Replace("/<agiv:STRAATNM>", "");
                                }
                                if (line.Contains("<agiv:HUISNR>"))
                                {
                                    // Keep lines that does not match
                                    huisnummer_T = line.Replace("<agiv:HUISNR>", "");
                                    huisnummer_T = huisnummer_T.Replace("/<agiv:HUISNR>", "");
                                }
                                if (line.Contains("<agiv:APPTNR>"))
                                {
                                    // Keep lines that does not match
                                    appartementnummer_T = line.Replace("<agiv:APPTNR>", "");
                                    appartementnummer_T = appartementnummer_T.Replace("/>", "");
                                }
                                if (line.Contains("<agiv:BUSNR>"))
                                {
                                    // Keep lines that does not match
                                    busnummer_T = line.Replace("<agiv:BUSNR>", "");
                                    busnummer_T = busnummer_T.Replace("/>", "");
                                }
                                if (line.Contains("<agiv:HNRLABEL>"))
                                {
                                    // Keep lines that does not match
                                    huisnummerlabel_T = line.Replace("<agiv:HNRLABEL>", "");
                                    huisnummerlabel_T = huisnummerlabel_T.Replace("/<agiv:HNRLABEL>", "");
                                }
                                if (line.Contains("<agiv:coord>"))
                                {
                                    if (line.Contains("<gml:X>"))
                                    {
                                        d1_T = line.Replace("<gml:X>", "");
                                        d1_T = d1_T.Replace("/<gml:X>", "");

                                        if (line.Contains("<gml:Y>"))
                                        {
                                            d2_T = line.Replace("<gml:Y>", "");
                                            d2_T = d2_T.Replace("/<gml:Y>", "");
                                        }

                                        // Adres a = new Adres(Convert.ToInt32(id_T),  straatnaam, huisnummer_T,  appartementnummer_T, busnummer_T, huisnummerlabel_T,  gemeente, Convert.ToInt32(postcode_T), Convert.ToDouble(d1_T), Convert.ToDouble(d2_T));
                                        string queryS = "INSERT INTO dbo.Adres( id, straatnaam, huisnummer, appartementnummer, busnummer, huisnummerlabel, adreslocatieID) " +
                        "VALUES( " + Convert.ToInt32(id_T)
                                    + straatnaam + huisnummer_T + appartementnummer_T + busnummer_T + huisnummer_T + huisnummerlabel_T + postcode_T
                                    + Convert.ToDouble(d1_T)
                                    + Convert.ToDouble(d2_T)
                                    + " );";


                                        SqlCommand myCommand = new SqlCommand(queryS, getConnection());
                                        myCommand.ExecuteNonQuery();
                                    }




                                }

                            }

                            getConnection().Close();
                        }
                    }
            }
            catch (IOException Exception)
            {
                Console.Write(Exception);
            }
        }
        

        #endregion

        #region VoegAdrestoe code

        /*Daarnaast wensen we in deze klasse ook een methode terug te vinden die:
                 -een nieuw adres toevoegt aan de databank.*/

        public void voegAdresToe(Adres a)
        {
            SqlConnection connection = getConnection();
            string queryS = "INSERT INTO dbo.Adres( huisnummer, appartementnummer, busnummer, huisnummerlabel, adreslocatieID) " +
                "VALUES(  @huisnummer ,@appartementnummer, @busnummer, @huisnummerlabel, @adreslocatieID)";

            string querySC = "INSERT INTO dbo.Adreslocatie(adreslocatieID,x,y) VALUES(@Id ,@x,@y )";

            using (SqlCommand command1 = connection.CreateCommand())
            using (SqlCommand command2 = connection.CreateCommand())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                command1.Transaction = transaction;
                command2.Transaction = transaction;
                try
                { //adres toevoegen
                    SqlParameter parhuisnummer = new SqlParameter();
                    parhuisnummer.ParameterName = "@huisnummer";
                    parhuisnummer.SqlDbType = SqlDbType.NVarChar;
                    command1.Parameters.Add(parhuisnummer);

                    SqlParameter parAppartementnummer = new SqlParameter();
                    parAppartementnummer.ParameterName = "@appartementnummer";
                    parAppartementnummer.SqlDbType = SqlDbType.NVarChar;
                    command1.Parameters.Add(parAppartementnummer);

                    SqlParameter parbusnummer = new SqlParameter();
                    parbusnummer.ParameterName = "@busnummer";
                    parbusnummer.SqlDbType = SqlDbType.NVarChar;
                    command1.Parameters.Add(parbusnummer);

                    SqlParameter parhuisnummerlabel = new SqlParameter();
                    parhuisnummerlabel.ParameterName = "@huisnummerlabel";
                    parhuisnummerlabel.SqlDbType = SqlDbType.NVarChar;
                    command1.Parameters.Add(parhuisnummerlabel);

                    SqlParameter paradreslocatieID = new SqlParameter();
                    paradreslocatieID.ParameterName = "@adreslocatieID";
                    paradreslocatieID.SqlDbType = SqlDbType.Int;
                    command1.Parameters.Add(paradreslocatieID);

                    command1.CommandText = queryS;

                    command1.Parameters["@huisnummer"].Value = a.huisnummer;
                    command1.Parameters["@appartementnummer"].Value = a.appartementnummer;
                    command1.Parameters["@busnummer"].Value = a.busnummer;
                    command1.Parameters["@huisnummerlabel"].Value = a.huisnummerlabel;
                    command1.Parameters["@adreslocatieID"].Value = a.locatie.Id;

                    //command1.ExecuteNonQuery();
                    int newID = (int)command1.ExecuteScalar();

                    //AdresLocatie toevoegen
                    command2.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    command2.Parameters.Add(new SqlParameter("@x", SqlDbType.Float));
                    command2.Parameters.Add(new SqlParameter("@y", SqlDbType.Float));

                    command2.CommandText = querySC;
                    command2.Parameters["@adreslocatieID"].Value = newID;
                    command2.Parameters["@x"].Value = a.locatie.x;
                    command2.Parameters["@y"].Value = a.locatie.y;

                    command2.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        #endregion VoegAdrestoe code
    }
}