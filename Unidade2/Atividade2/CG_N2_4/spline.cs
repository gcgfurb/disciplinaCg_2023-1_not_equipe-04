using OpenTK.Graphics.OpenGL4;
using CG_Biblioteca;

namespace gcgcg
{
  internal class Spline : Objeto
  {
    private int qtdPontos;

    public Spline(Objeto paiRef, Ponto4D pto1, Ponto4D pto2, Ponto4D pto3, Ponto4D pto4, int qtdPontos) : base(paiRef)
    {
      PrimitivaTipo = PrimitiveType.LineStrip;
      PrimitivaTamanho = 20;

      this.qtdPontos = qtdPontos;

      base.PontosAdicionar(pto1);
      base.PontosAdicionar(pto2);
      base.PontosAdicionar(pto3);
      base.PontosAdicionar(pto4);
      
      Atualizar();
    }

    public void Atualizar()
    {
      base.ObjetoAtualizar();
    }

    public Ponto4D GetSplinePoints(float t)
     {
            // (1-t)2 p0 + 2(1-t)tp1 + t2p2
            //   u            u         tt
            //  uu * p0  +  2 * u * t * p1 + tt * p2

            Ponto4D p0, p1, p2, p3;
             p0 = base.pontosLista[0];
             p1 = base.pontosLista[1];
             p2 = base.pontosLista[2];
             p3 = base.pontosLista[3];

             float tt = t * t;
             float ttt = tt * t;
             float u = 1 - t;
             float uu = u * u;
             float uuu = uu * u;

             double pointX = uuu * p0.X;
             pointX += 3 * uu * t * p1.X;
             pointX += 3 * u * tt * p2.X;
             pointX += ttt * p3.X;

             double pointY = uuu * p0.Y;
             pointY += 3 * uu * t * p1.Y;
             pointY += 3 * u * tt * p2.Y;
             pointY += ttt * p3.Y;

             return new Ponto4D(pointX, pointY);
        }

#if CG_Debug
    public override string ToString()
    {
      string retorno;
      retorno = "__ Objeto Spline _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
      retorno += base.ImprimeToString();
      return (retorno);
    }
#endif
  }
}
