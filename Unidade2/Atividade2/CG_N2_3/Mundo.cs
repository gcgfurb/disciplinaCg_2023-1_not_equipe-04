﻿#define CG_Gizmo  // debugar gráfico.
#define CG_OpenGL // render OpenGL.
//#define CG_DirectX // render DirectX.
// #define CG_Privado // código do professor.

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using OpenTK.Mathematics;

//FIXME: padrão Singleton

namespace gcgcg
{
  public class Mundo : GameWindow
  {
    private List<Objeto> objetosLista = new List<Objeto>();
    private Objeto objetoSelecionado = null;
    private char rotulo = '@';

    private readonly float[] _sruEixos =
    {
      -0.5f,  0.0f,  0.0f, /* X- */      0.5f,  0.0f,  0.0f, /* X+ */
       0.0f, -0.5f,  0.0f, /* Y- */      0.0f,  0.5f,  0.0f, /* Y+ */
       0.0f,  0.0f, -0.5f, /* Z- */      0.0f,  0.0f,  0.5f, /* Z+ */
    };

    private int _vertexBufferObject_sruEixos;
    private int _vertexArrayObject_sruEixos;

    private Shader _shaderVermelha;
    private Shader _shaderVerde;
    private Shader _shaderAzul;

    private bool _firstMove = true;
    private Vector2 _lastPos;


    private double angulo {get; set;}

    private double raio {get; set;}

    private Ponto4D SRPY {get; set;}

    private Ponto4D SRPX {get; set;}

    public Mundo(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
           : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    private void ObjetoNovo(Objeto objeto, Objeto objetoFilho = null)
    {
      if (objetoFilho == null)
      {
        objetosLista.Add(objeto);
        objeto.Rotulo = rotulo = Utilitario.charProximo(rotulo);
        objeto.ObjetoAtualizar();
        objetoSelecionado = objeto;
      }
      else
      {
        objeto.FilhoAdicionar(objetoFilho);
        objetoFilho.Rotulo = rotulo = Utilitario.charProximo(rotulo);
        objetoFilho.ObjetoAtualizar();
        objetoSelecionado = objetoFilho;
      }
    }

    protected override void OnLoad()
    {
      base.OnLoad();

      GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

      // Eixos
      _vertexBufferObject_sruEixos = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_sruEixos);
      GL.BufferData(BufferTarget.ArrayBuffer, _sruEixos.Length * sizeof(float), _sruEixos, BufferUsageHint.StaticDraw);
      _vertexArrayObject_sruEixos = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject_sruEixos);
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);
      _shaderVermelha = new Shader("Shaders/shader.vert", "Shaders/shaderVermelha.frag");
      _shaderVerde = new Shader("Shaders/shader.vert", "Shaders/shaderVerde.frag");
      _shaderAzul = new Shader("Shaders/shader.vert", "Shaders/shaderAzul.frag");

      Objeto objetoNovo = null;

       angulo = 45;
       raio = 0.5;

      SRPX = new Ponto4D(0,0);
      SRPY = Matematica.GerarPtosCirculo(angulo, raio);
         

      #region Objeto: segmento de reta  
      objetoNovo = new SegReta(null, SRPX, SRPY);
      ObjetoNovo(objetoNovo); objetoNovo = null;
      //objetosLista.Add(objetoNovo);
      //objetoSelecionado = objetoNovo ;

      #endregion
 

       

      



    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit);

#if CG_Gizmo      
      Sru3D();
#endif
      for (var i = 0; i < objetosLista.Count; i++)
        objetosLista[i].Desenhar();

      SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);

#region Teclado

        var input = KeyboardState;
        if (input.IsKeyDown(Keys.Escape))
        {
            Close();
        }
        else
        {
            // Movimento para os lados
            if (input.IsKeyDown(Keys.Q))
            { 
                SRPY.X-=.0001;
                SRPX.X-=.0001;
               // objetoSelecionado.PontosAlterar(SRPX, 0);
               // objetoSelecionado.PontosAlterar(SRPY, 1);
               
            }
            else if (input.IsKeyDown(Keys.W))
            {
                SRPX.X+=0.0001;
                SRPY.X+=0.0001;
             //  objetoSelecionado.PontosAlterar(SRPX, 0);
             //   objetoSelecionado.PontosAlterar(SRPY, 1);
           
            }

            // Mudança de tamanho (raio)
            else if (input.IsKeyDown(Keys.A))
            {
            
                raio -= 0.001; 
                Ponto4D SRPYN = Matematica.GerarPtosCirculo(angulo, raio);
                 SRPY.X = SRPYN.X;
                SRPY.Y = SRPYN.Y;
              //  objetoSelecionado.PontosAlterar(SRPY, 1);
          
            }
            else if (input.IsKeyDown(Keys.S))
            {
                raio += 0.001; 
                Ponto4D SRPYN = Matematica.GerarPtosCirculo(angulo, raio);
                 SRPY.X = SRPYN.X;
                SRPY.Y = SRPYN.Y;
               // objetoSelecionado.PontosAlterar(SRPX, 1);
            
            }

            // Mudança de ângulo
            else if (input.IsKeyDown(Keys.Z))
            {
                angulo -= 0.01;
                Ponto4D SRPYN = Matematica.GerarPtosCirculo(angulo, raio);
                SRPY.X = SRPYN.X;
                SRPY.Y = SRPYN.Y;
              //objetoSelecionado.PontosAlterar(SRPYN, 1);
             
            }
            else if (input.IsKeyDown(Keys.X))
            {
                angulo += 0.01;
                Ponto4D SRPYN = Matematica.GerarPtosCirculo(angulo, raio);
                 SRPY.X = SRPYN.X;
                SRPY.Y = SRPYN.Y;
            //    objetoSelecionado.PontosAlterar(SRPYN, 1);
             
            }
            objetoSelecionado.ObjetoAtualizar();
        }
        #endregion


      #region  Mouse
      var mouse = MouseState;
      // Mouse FIXME: inverte eixo Y, fazer NDC para proporção em tela
      Vector2i janela = this.ClientRectangle.Size;

      if (input.IsKeyDown(Keys.LeftShift))
      {
        if (_firstMove)
        {
          _lastPos = new Vector2(mouse.X, mouse.Y);
          _firstMove = false;
        }
        else
        {
          var deltaX = (mouse.X - _lastPos.X) / janela.X;
          var deltaY = (mouse.Y - _lastPos.Y) / janela.Y;
          _lastPos = new Vector2(mouse.X, mouse.Y);

          objetoSelecionado.PontosAlterar(new Ponto4D(objetoSelecionado.PontosId(0).X + deltaX, objetoSelecionado.PontosId(0).Y + deltaY, 0), 0);
          objetoSelecionado.ObjetoAtualizar();
        }
      }
      if (input.IsKeyDown(Keys.RightShift))
      {
        objetoSelecionado.PontosAlterar(new Ponto4D(mouse.X / janela.X, mouse.Y / janela.Y, 0), 0);
        objetoSelecionado.ObjetoAtualizar();
      }
      #endregion

    }

    protected override void OnResize(ResizeEventArgs e)
    {
      base.OnResize(e);

      GL.Viewport(0, 0, Size.X, Size.Y);
    }

    protected override void OnUnload()
    {
      foreach (var objeto in objetosLista)
      {
        objeto.OnUnload();
      }

      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      GL.BindVertexArray(0);
      GL.UseProgram(0);

      GL.DeleteBuffer(_vertexBufferObject_sruEixos);
      GL.DeleteVertexArray(_vertexArrayObject_sruEixos);

      GL.DeleteProgram(_shaderVermelha.Handle);
      GL.DeleteProgram(_shaderVerde.Handle);
      GL.DeleteProgram(_shaderAzul.Handle);

      base.OnUnload();
    }

#if CG_Gizmo
    private void Sru3D()
    {
#if CG_OpenGL && !CG_DirectX
      GL.BindVertexArray(_vertexArrayObject_sruEixos);
      // EixoX
      _shaderVermelha.Use();
      GL.DrawArrays(PrimitiveType.Lines, 0, 2);
      // EixoY
      _shaderVerde.Use();
      GL.DrawArrays(PrimitiveType.Lines, 2, 2);
      // EixoZ
      _shaderAzul.Use();
      GL.DrawArrays(PrimitiveType.Lines, 4, 2);
#elif CG_DirectX && !CG_OpenGL
      Console.WriteLine(" .. Coloque aqui o seu código em DirectX");
#elif (CG_DirectX && CG_OpenGL) || (!CG_DirectX && !CG_OpenGL)
      Console.WriteLine(" .. ERRO de Render - escolha OpenGL ou DirectX !!");
#endif
    }
#endif    

  }
}
