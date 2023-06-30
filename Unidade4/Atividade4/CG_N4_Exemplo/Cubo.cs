//https://github.com/mono/opentk/blob/main/Source/Examples/Shapes/Old/Cube.cs

#define CG_Debug
using CG_Biblioteca;

namespace gcgcg
{
  internal class Cubo : Objeto
  {
    Ponto4D[] pontos;

    public Cubo(Objeto paiRef, ref char _rotulo) :
      this(paiRef, ref _rotulo, new Ponto4D(-0.5, -0.5), new Ponto4D(0.5, 0.5))
    { }

    public Cubo(Objeto paiRef, ref char _rotulo, Ponto4D ptoInfEsq, Ponto4D ptoSupDir) : base(paiRef, ref _rotulo)
    {
      pontos = new Ponto4D[]
      {
        new Ponto4D(-0.2, -0.2,  0.2), // 0 - frente baixo esq
        new Ponto4D( 0.2, -0.2,  0.2), // 1 - frente baixo dir
        new Ponto4D( 0.2,  0.2,  0.2), // 2 - frente cima dir
        new Ponto4D(-0.2,  0.2,  0.2), // 3 - frente cima esq
        new Ponto4D(-0.2, -0.2, -0.2), // 4 - atrás baixo esq
        new Ponto4D( 0.2, -0.2, -0.2), // 5 - atrás baixo dir
        new Ponto4D( 0.2,  0.2, -0.2), // 6 - atrás cima dir
        new Ponto4D(-0.2,  0.2, -0.2)  // 7 - atrás cima esq
      };

    int[] ponto = {
      0,1,2,3,0,
      2,1,5,6,2,
      6,5,4,7,6,
      0,3,7,4,0,
      3,2,6,7,3,
      0,4,5,1,0
    };
    
    int n = 0;
    for(int i = 0; i <= 6; i++){
      base.PontosAdicionar(pontos[ponto[n+0]]);
      base.PontosAdicionar(pontos[ponto[n+1]]);
      base.PontosAdicionar(pontos[ponto[n+2]]);
      base.PontosAdicionar(pontos[ponto[n+3]]);
      base.PontosAdicionar(pontos[ponto[n+4]]);
      n = i*5;
    }
      Atualizar();
    }

    private void Atualizar()
    {

      base.ObjetoAtualizar();
    }
    

#if CG_Debug
    public override string ToString()
    {
      string retorno;
      retorno = "__ Objeto Cubo _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
      retorno += base.ImprimeToString();
      return (retorno);
    }
#endif

  }
}