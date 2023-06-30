//https://github.com/mono/opentk/blob/main/Source/Examples/Shapes/Old/Cube.cs

#define CG_Debug
using CG_Biblioteca;
using OpenTK.Mathematics;
using System.Drawing;

namespace gcgcg
{
  internal class Cubo : Objeto
  {
     Vector3[] vertices;
     int[] indices;
     Vector3[] normals;
     int[] colors;

    public Cubo(Objeto paiRef, ref char _rotulo) :
      this(paiRef, ref _rotulo, new Ponto4D(-0.5, -0.5), new Ponto4D(0.5, 0.5))
    { }
    public Cubo(Objeto paiRef, ref char _rotulo, Ponto4D ptoInfEsq, Ponto4D ptoSupDir) : base(paiRef, ref _rotulo)
    {
       vertices = new Vector3[]
       {
         new Vector3(-1.0f, -1.0f,  1.0f),
         new Vector3( 1.0f, -1.0f,  1.0f),
         new Vector3( 1.0f,  1.0f,  1.0f),
         new Vector3(-1.0f,  1.0f,  1.0f),
         new Vector3(-1.0f, -1.0f, -1.0f),
         new Vector3( 1.0f, -1.0f, -1.0f),
         new Vector3( 1.0f,  1.0f, -1.0f),
         new Vector3(-1.0f,  1.0f, -1.0f)
       };

       indices = new int[]
       {
         0, 1, 2, 2, 3, 0, // front face
         3, 2, 6, 6, 7, 3, // top face
         7, 6, 5, 5, 4, 7, // back face
         4, 0, 3, 3, 7, 4, // left face
         0, 1, 5, 5, 4, 0, // bottom face  
         1, 5, 6, 6, 2, 1, // right face
       };
       /*
            rect(vertices[indices[1]], -> indices[1]
             vertices[indices[1 + 1]], -> indices[2]
             vertices[indices[1 + 2]], -> indices[3]
             vertices[indices[1 + 3]], -> indices[4]
             colors[i]);
       */

       normals = new Vector3[]
       {
         new Vector3(-1.0f, -1.0f,  1.0f),
         new Vector3( 1.0f, -1.0f,  1.0f),
         new Vector3( 1.0f,  1.0f,  1.0f),
         new Vector3(-1.0f,  1.0f,  1.0f),
         new Vector3(-1.0f, -1.0f, -1.0f),
         new Vector3( 1.0f, -1.0f, -1.0f),
         new Vector3( 1.0f,  1.0f, -1.0f),
         new Vector3(-1.0f,  1.0f, -1.0f),
       };

       colors = new int[]
       {
         ColorToRgba32(Color.DarkRed),
         ColorToRgba32(Color.DarkRed),
         ColorToRgba32(Color.Gold),
         ColorToRgba32(Color.Gold),
         ColorToRgba32(Color.DarkRed),
         ColorToRgba32(Color.DarkRed),
         ColorToRgba32(Color.Gold),
         ColorToRgba32(Color.Gold),
       };
      //Criei este for
      //cada interação passa um lado do cubo
      for(int i = 0; i <= 6; i++)
      {
        int n = 6 * i;
        rect(vertices[indices[n + 0]], //0, 6, 12, 18
             vertices[indices[n + 1]], //1, 7, 13, ...
             vertices[indices[n + 2]], //2, 8, 14
             vertices[indices[n + 3]], //3, 9, 15
             vertices[indices[n + 4]], //4, 10, 16
             vertices[indices[n + 5]], //5, 11, 17
             colors[i]);
      }
      // Sentido horário
      /*
      base.PontosAdicionar(ptoInfEsq);
      base.PontosAdicionar(new Ponto4D(ptoSupDir.X, ptoInfEsq.Y));
      base.PontosAdicionar(ptoSupDir);
      base.PontosAdicionar(new Ponto4D(ptoInfEsq.X, ptoSupDir.Y));
      Atualizar();
      */
    }
    //Criei esta função
    //Desenha os lados do cubo
    public void rect(double[] p1, double[] p2, double[] p3,
                     double[] p4, double[] p5, double[] p6, int[] cor)
    // public void rect(float[] p1, float[] p2, float[] p3, float[] p4, int[] cor)
    //public void rect(Vector3[] p1, Vector3[] p2, Vector3[] p3, Vector3[] p4, int[] cor)
    {
      //Recebe dois triangulo
      //Ponto4D(double x = 0.0, double y = 0.0, double z = 0.0, double w = 1.0)
      GL.glTexCoord2f(0.0f, 0.0f);  //Coordenada textura
      Ponto4D ponto = new Ponto4D(p1[0], p1[1], p1[2], 1.0);
      base.PontosAdicionar(ponto);
      GL.glTexCoord2f(1.0f, 0.0f);  //Coordenada textura
      ponto = new Ponto4D(p2[0], p2[1], p2[2], 1.0);
      base.PontosAdicionar(ponto);
      GL.glTexCoord2f(1.0f, 1.0f);  //Coordenada textura
      ponto = new Ponto4D(p3[0], p3[1], p3[2], 1.0);
      base.PontosAdicionar(ponto);
      GL.glTexCoord2f(0.0f, 1.0f);  //Coordenada textura
      ponto = new Ponto4D(p4[0], p4[1], p4[2], 1.0);
      base.PontosAdicionar(ponto);
       GL.glTexCoord2f(0.0f, 1.0f);  //Coordenada textura
      ponto = new Ponto4D(p5[0], p5[1], p5[2], 1.0);
      base.PontosAdicionar(ponto);
       GL.glTexCoord2f(0.0f, 1.0f);  //Coordenada textura
      ponto = new Ponto4D(p6[0], p6[1], p6[2], 1.0);
      base.PontosAdicionar(ponto);
      Atualizar();
    }
    public static int ColorToRgba32(Color c)
    {
      return (int)((c.A << 24) | (c.B << 16) | (c.G << 8) | c.R);
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
