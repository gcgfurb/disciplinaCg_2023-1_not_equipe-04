#define CG_Debug

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
  internal class Circulo : Objeto
  {
   private double raio;
        private Ponto4D ptoCentro = new Ponto4D();

        public Circulo(Objeto paiRef, Ponto4D ptoCentro, double raio) : base(paiRef)
        {
            this.raio = raio;
            this.ptoCentro = ptoCentro;
            Ponto4D pto;
            
            for (int angulo = 0; angulo < 360; angulo += 5)
            {
                pto = Matematica.GerarPtosCirculo(angulo, raio);
                pto += ptoCentro;
                base.PontosAdicionar(pto);
            }    
      
         
        }

#if CG_Debug
    public override string ToString()
    {
      string retorno;
      retorno  = "__ Objeto Circulo _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
      retorno += base.ImprimeToString();
      return (retorno);
    }
#endif

  }
}
