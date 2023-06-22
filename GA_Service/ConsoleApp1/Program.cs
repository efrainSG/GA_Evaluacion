using System.Net;
using System.Text;
using System.Timers;

namespace ConsoleApp1
{
    public class Program
    {
        private static System.Timers.Timer timer;
        private static string apiURL = "https://localhost:7013/FCTProcessor";
        private static string path = "./";
        private static IEnumerable<FileInfo> files;
        private static bool Executing = false;

        static async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            bool isOk = true;
            Console.WriteLine("Iniciando...");
            try
            {
                sb.Append($"-----{DateTime.UtcNow}-----\n");
                if (!Executing)
                {
                    Executing = true;
                    DirectoryInfo dir = new DirectoryInfo($"{path}Pendientes\\");

                    files = dir.EnumerateFiles().Where(x => x.Extension.ToUpper().Equals(".FCT"));

                    DateTime Init = DateTime.Now;

                    using (HttpClient client = new HttpClient())
                    {
                        foreach (var item in files)
                        {
                            var contenido = File.ReadAllText(item.FullName);
                            string[] filas = contenido.Split("\n").Where(x => x.Length > 1).ToArray();

                            foreach (string fila in filas)
                            {
                                string[] datos = fila.Substring(3).Split("|");
                                var body = new
                                {
                                    idTienda = datos[0],
                                    idRegistradora = datos[1],
                                    ticket = int.Parse(datos[4]),
                                    fechaHora = $"{datos[2].Substring(0, 4)}-{datos[2].Substring(4, 2)}-{datos[2].Substring(6, 2)}T{datos[3].Substring(0, 2)}:{datos[3].Substring(2, 2)}:{datos[3].Substring(4, 2)}Z",
                                    impuesto = decimal.Parse(datos[5].Trim()),
                                    total = decimal.Parse(datos[6])
                                };

                                var json = Newtonsoft.Json.JsonConvert.SerializeObject(body);
                                var content = new StringContent(json, Encoding.UTF8, "application/json");

                                HttpResponseMessage response = await client.PostAsync(apiURL, content);
                                isOk &= (response.StatusCode == HttpStatusCode.OK);
                            }

                            if (isOk)
                            {
                                sb.Append($"{item.Name}: Ok\n");
                                item.MoveTo($"{path}Procesados\\{item.Name}");
                            }
                            else
                            {
                                sb.Append($"{item.Name}: Fallido\n");
                                item.MoveTo($"{path}Pendientes\\{item.Name}_error");
                            }
                            if (DateTime.Now.Subtract(Init).Seconds > 40)
                            {
                                break;
                            }
                        }
                    }
                    Executing = false;
                    Console.WriteLine("Saliendo...");
                }
            }
            catch (Exception ex)
            {
                sb.Append($"{ex.Message}\n");
                Executing = false;
            }
            finally
            {
                using (StreamWriter sw = new StreamWriter($"{path}log\\Log_{DateTime.UtcNow.ToString().Replace(":","").Replace("-","").Replace("/","").Replace(".","").Replace(" ","")}.txt", false))
                {
                    sw.Write(sb.ToString());
                }
            }
            Console.WriteLine("Terminando...");
        }
        static void Main(params string[] args)
        {
            path = args.Length > 0 ? args[0] : ".\\";
            DirectoryInfo dir = new DirectoryInfo(path);

            if (!dir.GetDirectories().Any(x => x.Name == "Log"))
            {
                dir.CreateSubdirectory("Log");
            }
            if (!dir.GetDirectories().Any(x => x.Name == "Procesados"))
            {
                dir.CreateSubdirectory("Procesados");
            }

            timer = new System.Timers.Timer(60_000);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Start();
            Console.ReadLine();
        }
    }
}