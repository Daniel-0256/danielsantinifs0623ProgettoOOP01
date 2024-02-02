using System;
using System.Text.RegularExpressions;

namespace ProgettoOOP01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inserisci il nome: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Inserisci il cognome: ");
            string cognome = Console.ReadLine();

            DateTime dataNascita = ValidaDataNascita();

            string codiceFiscale = ValidaCodiceFiscale();

            char sesso = ValidaSesso();

            Console.WriteLine("Inserisci il comune di residenza: ");
            string comuneResidenza = Console.ReadLine();

            decimal redditoAnnuale = GetValidRedditoAnnuale();

            Contribuente contribuente = new Contribuente(nome, cognome, dataNascita, codiceFiscale, sesso, comuneResidenza, redditoAnnuale);

            decimal imposta = contribuente.CalcolaImposta();

            Console.WriteLine("CALCOLO DELL'IMPOSTA DA VERSARE:");
            Console.WriteLine($"Contribuente: {contribuente.Nome} {contribuente.Cognome},");
            Console.WriteLine($"nato il {contribuente.DataNascita:dd/MM/yyyy} ({contribuente.Sesso}),");
            Console.WriteLine($"residente in {contribuente.ComuneResidenza},");
            Console.WriteLine($"codice fiscale: {contribuente.CodiceFiscale}");
            Console.WriteLine($"Reddito dichiarato: {contribuente.RedditoAnnuale:N2}");
            Console.WriteLine($"IMPOSTA DA VERSARE: {imposta:N2}");
        }

        class Contribuente
        {
            public string Nome { get; set; }
            public string Cognome { get; set; }
            public DateTime DataNascita { get; set; }
            public string CodiceFiscale { get; set; }
            public char Sesso { get; set; }
            public string ComuneResidenza { get; set; }
            public decimal RedditoAnnuale { get; set; }

            public Contribuente(string nome, string cognome, DateTime dataNascita, string codiceFiscale, char sesso, string comuneResidenza, decimal redditoAnnuale)
            {
                Nome = nome;
                Cognome = cognome;
                DataNascita = dataNascita;
                CodiceFiscale = codiceFiscale;
                Sesso = sesso;
                ComuneResidenza = comuneResidenza;
                RedditoAnnuale = redditoAnnuale;
            }

            public decimal CalcolaImposta()
            {
                decimal imposta = 0;

                if (RedditoAnnuale <= 15000)
                {
                    imposta = RedditoAnnuale * 0.23m;
                } 
                else if(RedditoAnnuale <= 28000)
                {
                    imposta = 3450 + (RedditoAnnuale - 15000) * 0.27m;
                }
                else if(RedditoAnnuale <= 55000)
                {
                    imposta = 6960 + (RedditoAnnuale - 28000) * 0.38m;
                }
                else if(RedditoAnnuale <= 75000)
                {
                    imposta = 17220 + (RedditoAnnuale - 55000) * 0.41m;
                }
                else
                {
                    imposta = 25420 + (RedditoAnnuale - 75000) * 0.43m;
                }

                return imposta;
            }
        }

        static DateTime ValidaDataNascita()
        {
            DateTime dataNascita;
            string formatoData = "dd/MM/yyyy";

            do
            {
                Console.WriteLine($"Inserisci la data di nascita nel formato {formatoData}: ");
                string inputData = Console.ReadLine();

                if (DateTime.TryParse(inputData, out dataNascita))
                {
                    break;
                }

                Console.WriteLine("Data di nascita non valida. Riprova.");
            } while (true);

            return dataNascita;
        }

        static string ValidaCodiceFiscale()
        {
            string codiceFiscale;
            do
            {
                Console.WriteLine("Inserisci il codice fiscale valido (16 caratteri): ");
                codiceFiscale = Console.ReadLine();

                if (codiceFiscale.Length == 16 && Regex.IsMatch(codiceFiscale, @"^[a-zA-Z0-9]+$"))
                {
                    break;
                }

                Console.WriteLine("Codice fiscale non valido. Riprova.");
            } while (true);

            return codiceFiscale;
        }

        static char ValidaSesso()
        {
            char sesso;
            do
            {
                Console.WriteLine("Inserisci il sesso (M/F): ");
                if (char.TryParse(Console.ReadLine(), out sesso) && (sesso == 'M' || sesso == 'F'))
                {
                    break;
                }

                Console.WriteLine("Sesso non valido. Riprova.");
            } while (true);

            return sesso;
        }

        static decimal GetValidRedditoAnnuale()
        {
            decimal redditoAnnuale;
            do
            {
                Console.WriteLine("Inserisci il reddito annuale: ");
                if (decimal.TryParse(Console.ReadLine(), out redditoAnnuale) && redditoAnnuale >= 0)
                {
                    break;
                }

                Console.WriteLine("Reddito annuale non valido. Riprova.");
            } while (true);

            return redditoAnnuale;
        }
    }
}
