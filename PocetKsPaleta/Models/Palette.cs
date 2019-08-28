using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocetKsPaleta
{
    class Palette
    {
        public double Apalette { get; set; }
        public double Bpalette { get; set; }
        public double Cpalette { get; set; }

        public double VolumePalette
        {
            get
            {
                return volumePalette = Apalette*Bpalette*Cpalette;
            }
        }
        public double PiecesPerPallete { get { return piecesPerPallete; } }
        public double WeightPalette{ get { return weightPalette;}}
        public double HeightPalette { get { return heightPalette; } }
        public int CountOfPalette { get { return countOfPalette; } }
        public double LastProductValue { get { return lastProductValue; } }
        public double CartonsPerPallete { get { return cartonsPerPallete; } }
        public int CountOfLastPalette { get { return countOfLastPalette; } }
        public double HeightLastPallete { get { return heightLastPallete; } }
        public double WeightLastPalette { get { return weightLastPalette; } }
        public double PrizePerPallete { get { return prizePerPallete; } }
        public double PrizePerLastPallete { get { return prizePerLastPallete; } }

        private double SumResult;
        private double lastProductValue;
        private double volumePalette;
        private double piecesPerPallete;
        private double weightPalette;
        private double heightPalette;
        private int countOfPalette;
        private double cartonsPerPallete;
        private int countOfLastPalette;
        private double heightLastPallete;
        private double weightLastPalette;
        private double prizePerPallete;
        private double prizePerLastPallete; 


        public void GetNumberOfPalettes(Product product)
        {
            int Sum1 = (int)Math.Floor((Apalette / (product.A))) * (int)Math.Floor((Bpalette / (product.B))) * (int)Math.Floor((Cpalette / (product.C))); 
            int Sum2 = (int)Math.Floor((Apalette / (product.A))) * (int)Math.Floor((Bpalette / (product.C))) * (int)Math.Floor((Cpalette / (product.B)));
            int Sum3 = (int)Math.Floor((Apalette / (product.B))) * (int)Math.Floor((Bpalette / (product.A))) * (int)Math.Floor((Cpalette / (product.C)));
            int Sum4 = (int)Math.Floor((Apalette / (product.B))) * (int)Math.Floor((Bpalette / (product.C))) * (int)Math.Floor((Cpalette / (product.A)));
            int Sum5 = (int)Math.Floor((Apalette / (product.C))) * (int)Math.Floor((Bpalette / (product.A))) * (int)Math.Floor((Cpalette / (product.B)));
            int Sum6 = (int)Math.Floor((Apalette / (product.C))) * (int)Math.Floor((Bpalette / (product.B))) * (int)Math.Floor((Cpalette / (product.A)));

            List<double> Sum = new List<double> { Sum1, Sum2, Sum3, Sum4, Sum5, Sum6 };

            //kolik kartonů se vejde na jednu paletu
            cartonsPerPallete = Sum.Max();
            //kolik ks se vleze na jednu paletu
            piecesPerPallete = (int)Math.Floor(Sum.Max() * product.MasterCartonPieces);
            weightPalette = product.Weight* PiecesPerPallete * product.MasterCartonPieces;
            countOfPalette = (int)Math.Ceiling((product.Count / product.MasterCartonPieces) / cartonsPerPallete);
            countOfLastPalette = (product.Count / product.MasterCartonPieces) % (int)cartonsPerPallete;
            weightLastPalette = CountOfLastPalette * product.Weight * product.MasterCartonPieces;
            prizePerPallete = piecesPerPallete * product.Prize * product.MasterCartonPieces;
            prizePerLastPallete = CountOfLastPalette * product.Prize *product.MasterCartonPieces;

            if (countOfLastPalette == 0)
            {
                countOfLastPalette = (int)cartonsPerPallete;
            }
             

            foreach (double sum in Sum)
            {
                if (SumResult < sum)
                {
                    SumResult = sum;
                    if (SumResult.Equals(Sum1))
                    {
                        lastProductValue = product.C;
                    }
                    if (SumResult.Equals(Sum2))
                    {
                        lastProductValue = product.B;
                    }
                    if (SumResult.Equals(Sum3))
                    {
                        lastProductValue = product.C;
                    }
                    if (SumResult.Equals(Sum4))
                    {
                        lastProductValue = product.A;
                    }
                    if (SumResult.Equals(Sum5))
                    {
                        lastProductValue = product.B;
                    }
                    if (SumResult.Equals(Sum6))
                    {
                        lastProductValue = product.A;
                    }
                }
            }
            heightPalette = lastProductValue * (int)Math.Floor(Cpalette / LastProductValue);
            heightLastPallete = ((int)Math.Ceiling(countOfLastPalette / (SumResult / (int)Math.Floor(Cpalette / LastProductValue)))) * lastProductValue;

        }
    }
}
