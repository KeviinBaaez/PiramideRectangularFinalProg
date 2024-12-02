using PiramideRectangularFinalProg.Entidades;

namespace PiramideRectangularFinalProg.Datos
{
    public class Repositorio
    {
        public List<Piramide> lista;
        public ArchivoSecuencial archivo;
        public List<Piramide> listaColor;
        public Repositorio()
        {
            lista = new List<Piramide>();
            archivo = new ArchivoSecuencial();
            lista = archivo.LeerDatos();
        }

        public (bool exito, string mensaje) AgregarPiramide(Piramide piramide)
        {
            var existe = lista.Any(p => p.LadoBase == piramide.LadoBase && p.Altura == piramide.Altura && p.CantidadLados == piramide.CantidadLados);
            if (!existe)
            {
                lista.Add(piramide);
                return (true, $"Piramide agregada\n {piramide.ToString()}");
            }
            else
            {
                return (false, "Piramide existente");
            }
        }

        public (bool exito, string mensaje) EditarPiramide(Piramide piramide, Piramide piramideEditada)
        {
            var existe = lista.Any(p => p.LadoBase == piramideEditada.LadoBase && p.Altura == piramideEditada.Altura && p.CantidadLados == piramideEditada.CantidadLados && p.Color == piramideEditada.Color && p.Material == piramideEditada.Material);
            if (!existe)
            {
                AgregarPiramide(piramideEditada);
                EliminarPiramide(piramide);
                return (true, $"Piramide editada\n {piramideEditada.ToString()}");
            }
            else
            {
                return (false, "Piramide existente");
            }
        }

        public (bool exito, string mensaje) EliminarPiramide(Piramide piramide)
        {
            var existe = lista.Any(p => p.LadoBase == piramide.LadoBase && p.Altura == piramide.Altura && p.CantidadLados == piramide.CantidadLados);
            if (existe)
            {
                int index = 0;
                foreach (var p in lista)
                {
                    if(p.LadoBase != piramide.LadoBase && p.Altura != piramide.Altura && p.CantidadLados != piramide.CantidadLados)
                    {
                        index++;
                    }
                    else
                    {
                        index = index;
                        break;
                    }
                }
                lista.RemoveAt(index);
                return (true, "Piramide Eliminada");
            }
            else
            {
                return (false, "La piramide no existe");
            }
        }

        public (bool exito, List<Piramide>) GetListaColor(Colores colorSeleccionado)
        {
            var existe = lista.Any(p => p.Color == colorSeleccionado);
            if (existe)
            {
                listaColor = new List<Piramide>();
                foreach (var item in lista)
                {
                    if(item.Color == colorSeleccionado)
                    {
                        listaColor.Add(item);
                    }
                }
                return (true, listaColor);
            }
            else
            {
                return (false, lista);
            }
        }

        public int GetCantidad()
        {
            return lista.Count();
        }

        public void GuardarDatos()
        {
            archivo.GuardarDatos(lista);
        }
    }
}
