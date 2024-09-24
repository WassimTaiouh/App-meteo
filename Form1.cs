using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading;
using Meteo;

namespace Meteo
{
    public partial class Weatherform : Form
    {
        public Weatherform()
        {
            InitializeComponent();
        }
        //Lance la recherche de données météos
        private void buttonOK_OnClick(object sender, EventArgs e)
        {
            loadData();
        }
        //API Météo (weatherstack.com)
        private void loadData()
        {
            //clé de l'API
            string APIKey = "4204cb72b9ef4f6663e15420a02a25bf";
            //Appel l'API
            using (WebClient client = new WebClient())
            {
                //Accès à l'API
                string baseurl = string.Format("http://api.weatherstack.com/current?" + $"access_key={APIKey}&query={nomville.Text}");
                var json = client.DownloadString(baseurl);
                //Conversion du Json en c#
                Meteoinfo.root Info = JsonConvert.DeserializeObject<Meteoinfo.root>(json);
                //Afficher les valeurs de l'API sur l'interface graphique à partir de la classe Meteoinfo (ficher Info_meteo.cs)
                lville.Text = Info.location.name; //Le nom de la ville dans le label lville
                ldateheure.Text = Info.location.localtime.ToString(); //la date et l'heure de la ville 
                ldonneedirectionvent.Text = Info.current.wind_dir.ToString(); // direction du vent (nord ouest, sud est etc etc)
                ldonneehumidite.Text = Info.current.humidity.ToString() + "%"; // taux d'humidité de la ville
                ldonneenuageux.Text = Info.current.cloudcover.ToString() + "%"; //taux de nuages de la ville
                ldonneepression.Text = Info.current.pressure.ToString() + "hPa"; //pression de la ville 
                ldonneevitessevent.Text = Info.current.wind_speed.ToString() + "km/h"; //vitesse du vent de la ville
                ldonneetemperature.Text = Info.current.temperature.ToString() + "°C" ; //la temperature de la ville
                //Donnée du taux nuageux
                string status = ltemps.Text; //Le status correspond au temps de la ville
                int tauxnuage = (int)Info.current.cloudcover; // tauxnuage correspond à l'état nuageux de la ville
                //If else : le temps écris l'état nuageux de la ville
                if (tauxnuage > 50)
                {
                    ldonneenuageux.Text = Info.current.cloudcover.ToString();
                    ltemps.Text = "nuageux";
                }
                else if (tauxnuage > 25)
                {
                    ldonneenuageux.Text = Info.current.cloudcover.ToString();
                    ltemps.Text = "partiellement couvert";
                }
                else if (tauxnuage < 25)
                {
                    ldonneenuageux.Text = Info.current.cloudcover.ToString();
                    ltemps.Text = "ensoleille";
                }
                //Switch case : une image illustre l'état météorologique de la ville
                switch (status)
                {
                    case "nuageux":
                        pictureBox1.Image = Image.FromFile("C:/Wassim/CDA/Projet/app_meteo1/Meteo/image/nuageux.png");
                        break;
                    case "ensoleille":
                        pictureBox1.Image = Image.FromFile("C:/Wassim/CDA/Projet/app_meteo1/Meteo/image/ensoleille.png");
                        break;
                    case "brouillard":
                        pictureBox1.Image = Image.FromFile("C:/Wassim/CDA/Projet/app_meteo1/Meteo/image/brouillard.png");
                        break;
                    case "neige attendue":
                        pictureBox1.Image = Image.FromFile("C:/Wassim/CDA/Projet/app_meteo1/Meteo/image/neige.png");
                        break;
                    case "grêle attendue":
                        pictureBox1.Image = Image.FromFile("C:/Wassim/CDA/Projet/app_meteo1/Meteo/image/grele.png");
                        break;
                    case "averses éparses":
                        pictureBox1.Image = Image.FromFile("C:/Wassim/CDA/Projet/app_meteo1/Meteo/image/averses_eparse.png");
                        break;
                    case "partiellement couvert":
                        pictureBox1.Image = Image.FromFile("C:/Wassim/CDA/Projet/app_meteo1/Meteo/image/partiellement_couvert.png");
                        break;
                    case "averses":
                        pictureBox1.Image = Image.FromFile("C:/Wassim/CDA/Projet/app_meteo1/Meteo/image/averse.png");
                        break;
                    case "nuit":
                        pictureBox1.Image = Image.FromFile("C:/Wassim/CDA/Projet/app_meteo1/Meteo/image/nuit.png");
                        break;
                    case "orageux":
                        pictureBox1.Image = Image.FromFile("C:/Wassim/CDA/Projet/app_meteo1/Meteo/image/orage.png");
                        break;
                }
                //Image illustrant l'état pluvieux de la ville
                if (Info.current.precip != 0)
                {
                    pictureBox1.Image = Image.FromFile("C:/Wassim/CDA/Projet/app_meteo1/Meteo/image/legere_averse.png");
                    ltemps.Text = "legere averse";
                }
                else if (Info.current.precip > 50)
                {
                    pictureBox1.Image = Image.FromFile("C:/Wassim/CDA/Projet/app_meteo1/Meteo/image/averse");
                    ltemps.Text = "averse";
                }
            }
        }
    }
}

