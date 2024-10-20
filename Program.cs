using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform;

namespace Proiect
{
    
    class SimpleWindow : GameWindow
    {   
        float constantaX = 0f;
        float constantaY = 0f;
        float mouseX = 0.0f;    
        float mouseY = 0.0f;
        // Constructor.
        public SimpleWindow() : base(1024, 800)
        {
            KeyDown += Keyboard_KeyDown;
            MouseMove += Move_Mouse;
        }

        // Tratează evenimentul generat de apăsarea unei taste. Mecanismul standard oferit de .NET
        // este cel utilizat.
        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();
            if (e.Key == Key.D)
                constantaX += 0.1f;
            if (e.Key == Key.A)
                constantaX -= 0.1f;    
            if (e.Key == Key.F11)
                if (this.WindowState == WindowState.Fullscreen)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Fullscreen;
        }
        void Move_Mouse(object sender,MouseEventArgs e)
        {

            mouseX = e.X;
            mouseY = e.Y;
            if (mouseY < 540)
                constantaY += 0.001f;
            if(mouseY > 540)
                constantaY -= 0.001f;
            





        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.LightGreen);
            
        }

        
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }

        // Secțiunea pentru "game logic"/"business logic". Tot ce se execută în această secțiune va fi randat
        // automat pe ecran în pasul următor - control utilizator, actualizarea poziției obiectelor, etc.
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // Momentan aplicația nu face nimic!
        }

        // Secțiunea pentru randarea scenei 3D. Controlată de modulul logic din metoda ONUPDATEFRAME.
        // Parametrul de intrare "e" conține informatii de timing pentru randare.
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Modul imediat! Suportat până la OpenGL 3.5 (este ineficient din cauza multiplelor apeluri de
            // funcții).
       
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Black);
            GL.Vertex2(-0.9f+constantaX, -0.9f +constantaY);
            GL.Color3(Color.DarkGray);
            GL.Vertex2(-0.5f + constantaX, 0.5f + constantaY);
            GL.Color3(Color.DarkRed);
            GL.Vertex2(0.5f + constantaX, 0.5f + constantaY);
            GL.Color3(Color.DarkGreen);
            GL.Vertex2(0.9f + constantaX, -0.9f + constantaY);
            
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Black);
            GL.Vertex2(-0.95f, -0.95f);
            GL.Vertex2(0.95f, 0.95f);
            GL.End();

            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Black);
            GL.Vertex2(-0.95f, 0.95f);
            GL.Vertex2(0.95f, -0.95f);
            GL.End();
           
            
            // Sfârșitul modului imediat!

            this.SwapBuffers();
        }

        [STAThread]
        static void Main(string[] args)
        {

            // Utilizarea cuvântului-cheie "using" va permite dealocarea memoriei o dată ce obiectul nu mai este
            // în uz (vezi metoda "Dispose()").
            // Metoda "Run()" specifică cerința noastră de a avea 30 de evenimente de tip UpdateFrame per secundă
            // și un număr nelimitat de evenimente de tip randare 3D per secundă (maximul suportat de subsistemul
            // grafic). Asta nu înseamnă că vor primi garantat respectivele valori!!!
            // Ideal ar fi ca după fiecare UpdateFrame să avem si un RenderFrame astfel încât toate obiectele generate
            // în scena 3D să fie actualizate fără pierderi (desincronizări între logica aplicației și imaginea randată
            // în final pe ecran).
            using (SimpleWindow example = new SimpleWindow())
            {

                // Verificați semnătura funcției în documentația inline oferită de IntelliSense!
                example.Run(30.0, 0.0);
            }
        }
    }
}
