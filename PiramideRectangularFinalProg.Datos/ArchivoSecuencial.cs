using PiramideRectangularFinalProg.Entidades;

namespace PiramideRectangularFinalProg.Datos
{
    public class ArchivoSecuencial
    {
        private string nombreArchivo = "Piramides.txt";
        private string nombreRuta = Environment.CurrentDirectory;
        private string rutaCompleta;

        public ArchivoSecuencial()
        {
            rutaCompleta = Path.Combine(nombreRuta, nombreArchivo);
        }

        public void GuardarDatos(List<Piramide> datos)
        {
            using(var escritor = new StreamWriter(rutaCompleta))
            {
                foreach (var item in datos)
                {
                    var linea = ConstruirLinea(item);
                    escritor.WriteLine(linea);  
                }
            }
        }

        private object ConstruirLinea(Piramide item)
        {
            string linea = $"{item.LadoBase} | { item.CantidadLados} | {item.Altura} | {item.Color.GetHashCode()} | {item.Material.GetHashCode()}";
            return linea;
        }

        public List<Piramide> LeerDatos()
        {
            List<Piramide> lista = new List<Piramide>();
            if (!File.Exists(rutaCompleta))
            {
                return lista;
            }
            using(var lector = new StreamReader(rutaCompleta))
            {
                while (!lector.EndOfStream)
                {
                    var linea = lector.ReadLine();
                    Piramide piramide = ConstruirPiramide(linea);
                    lista.Add(piramide);
                }
            }
            return lista;
        }

        private Piramide ConstruirPiramide(string? linea)
        {
            var campos = linea.Split('|');
            int lado = int.Parse(campos[0]);
            int cantidadLado = int.Parse(campos[1]);
            int altura = int.Parse(campos[2]);
            Colores color = (Colores)int.Parse(campos[3]);
            Material material = (Material)int.Parse(campos[4]);

            return new Piramide(lado, altura, cantidadLado, color, material);
        }
    }
}
