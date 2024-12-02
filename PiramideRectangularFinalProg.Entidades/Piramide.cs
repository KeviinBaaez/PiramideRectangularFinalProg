using System.Data.SqlTypes;
using System.Text;

namespace PiramideRectangularFinalProg.Entidades
{
    public class Piramide
    {
		private int ladoBase;
		private int altura;
		private int cantidadLados;
		private Colores color;
		private Material material;

        public Piramide()
        {
            
        }
        public Piramide(int ladoBase, int altura, int cantidadLados, Colores color, Material material)
        {
            LadoBase = ladoBase;
            Altura = altura;
            CantidadLados = cantidadLados;
            Color = color;
            Material = material;
        }

        public Material Material
		{
			get { return material; }
			set { material = value; }
		}


		public Colores Color
		{
			get { return color; }
			set { color = value; }
		}


		public int CantidadLados
		{
			get { return cantidadLados; }
			set
			{
				if(value == 3 ||value == 4)
				{
                    cantidadLados = value;
                }
			}
		}


		public int Altura
		{
			get { return altura; }
			set { altura = value; }
		}

		public int LadoBase
		{
			get { return ladoBase; }
			set { ladoBase = value; }
		}


		public double GetArea()
		{
			double area = 0;
			if(cantidadLados == 3)
			{
				area = Math.Sqrt(CantidadLados) / 4 * Math.Pow(LadoBase, 2);
				return area;
			}
			else
			{
				area = Math.Pow(LadoBase, 2);
			}
			return area;
		}

		public double GetVolumen()
		{
			double volumen = (GetArea() * Altura) / 3;
			return volumen;
		}

		public double GetPerimetro()
		{
			double perimetro = LadoBase * CantidadLados;
			return perimetro;
		}

		public double GetAlturaLateral()
		{
			double alturaLateral = Math.Sqrt(Math.Pow(Altura, 2) + Math.Pow((LadoBase / 2), 2));
			return alturaLateral;
		}

		public double GetAreaLateral()
		{
			double areaLateral = (GetPerimetro() * GetAlturaLateral())/ 2;
			return areaLateral;
		}

		public double GetAreaTotal()
		{
			var areaTotal = GetArea() + GetAreaLateral();
			return areaTotal;
		}

        public override string ToString()
        {
			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"Area base: {GetArea()}");
			sb.AppendLine($"Volumen: {GetVolumen()}");
            sb.AppendLine($"Perimetro: {GetPerimetro()}");
            sb.AppendLine($"Altura Lateral: {GetAlturaLateral()}");
            sb.AppendLine($"Area Lateral: {GetAreaLateral()}");
            sb.AppendLine($"Area Total: {GetAreaTotal()}");


			return sb.ToString();
        }
    }
}
