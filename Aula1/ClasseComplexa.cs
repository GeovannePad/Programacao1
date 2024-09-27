using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula1
{
    public class ClasseComplexa : IImprimirValores
    {
        private int PropInt { get; set; }
        protected bool PropBool { get; set; }
        public decimal MeuDecimal { get; set; }

        protected DateTime dataAtual;
        float floatField;

        public Direcoes Direcao { get; set; }
        public string PropInterface { get; set; }

        public enum Direcoes
        {
            Cima = 1,
            Baixo = 10,
            LadoEsquerdo = 20,
            LadoDireito = 67
        }
    }
}
