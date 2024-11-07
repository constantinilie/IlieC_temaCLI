using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        static float[][] culori = new float[3][];
        float constantaX = 0f;
        float constantaY = 0f;
        float mouseX = 0.0f;
        float mouseY = 0.0f;
        int punct = 0;
        float rotationX = 0.0f;
        float rotationY = 0.0f;

        public SimpleWindow() : base(1024, 800)
        {
            KeyDown += Keyboard_KeyDown;
            MouseMove += Move_Mouse;

            string[] liniiCuloare = File.ReadAllLines("Culori.txt"); //Initializeaza culorile cu cele din fisierul Culori.txt
            for (int i = 0; i < liniiCuloare.Length; i++)
            {
                string[] linieCuloare = liniiCuloare[i].Split(' ');
                culori[i] = new float[3];
                culori[i][0] = float.Parse(linieCuloare[0]);
                culori[i][1] = float.Parse(linieCuloare[1]);
                culori[i][2] = float.Parse(linieCuloare[2]);
            }
        }


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
            //selectam care colt sa si schimbe culoarea;
            if (e.Key == Key.Number1)
                punct = 0;
            if (e.Key == Key.Number2)
                punct = 1;
            if (e.Key == Key.Number3)
                punct = 2;
            // I,J- pentru punctul ROSU, O,K, pentru VERDE, P,L pentru ALBASTRU
            if (e.Key == Key.I)
                culori[punct][0] = Math.Min(culori[punct][0] + 0.1f, 1.0f);
            if (e.Key == Key.J)
                culori[punct][0] = Math.Max(culori[punct][0] - 0.1f, 0.0f);
            if (e.Key == Key.O)
                culori[punct][1] = Math.Min(culori[punct][1] + 0.1f, 1.0f);
            if (e.Key == Key.K)
                culori[punct][1] = Math.Max(culori[punct][1] - 0.1f, 0.0f);
            if (e.Key == Key.P)
                culori[punct][2] = Math.Min(culori[punct][2] + 0.1f, 1.0f);
            if (e.Key == Key.L)
                culori[punct][2] = Math.Max(culori[punct][2] - 0.1f, 0.0f);


            Console.WriteLine($"Punctul {punct + 1} - R: {culori[punct][0]}, G: {culori[punct][1]}, B: {culori[punct][2]}");



        }
        void Move_Mouse(object sender, MouseEventArgs e)
        {

            // Calculează delta-ul pe axele X și Y
            float deltaX = e.X - (Width / 2);
            float deltaY = e.Y - (Height / 2);

            // Modifică unghiurile pe baza mișcării mouse-ului
            rotationY += deltaX * 0.1f; // Sensibilitate pe axa Y
            rotationX -= deltaY * 0.1f; // Sensibilitate pe axa X

            // Limitează unghiurile pentru a preveni rotația completă
            rotationX = MathHelper.Clamp(rotationX, -89.0f, 89.0f);

            // Recentrează mouse-ul în fereastra de joc
            Mouse.SetPosition(Width / 2, Height / 2);


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


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // Momentan aplicația nu face nimic!
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.LoadIdentity();
            GL.Rotate(rotationX, 1.0f, 0.0f, 0.0f);
            GL.Rotate(rotationY, 0.0f, 1.0f, 0.0f);

            // Modul imediat! Suportat până la OpenGL 3.5 (este ineficient din cauza multiplelor apeluri de
            // funcții).
            /* EX1. TEMA3
            GL.Begin(PrimitiveType.Lines); // 
            // Axa X (Rosie)
            GL.Color3(1.0f, 0.0f, 0.0f);  // culoarea rosie
            GL.Vertex3(0f, 0.0f, 0.0f); // punct pornire axa Ox ( centru)
            GL.Vertex3(1.0f, 0.0f, 0.0f);  // punct final axa Ox (sus mijloc)

            // Axa Y (verde)
            GL.Color3(0.0f, 1.0f, 0.0f);  // culoarea verde
            GL.Vertex3(0.0f, 0.0f, 0.0f); // punct pornire axa Oy (centru)
            GL.Vertex3(0.0f, 1.0f, 0.0f);  //punct final axa Oy (jos mijloc)

            // Axa Z (albastra)
            GL.Color3(0.0f, 0.0f, 1.0f);  // culoarea albastra
            GL.Vertex3(0.0f, 0.0f, 0.0f); // punct pornire axa Oz ( centru)
            GL.Vertex3(-1f, -1f, 0.0f);  // punct final axa Oz( colt jos stanga)
            GL.End();
            */
            /*EX8. TEMA3*/
            string[] linii = File.ReadAllLines("Coordonate.txt");// am scris coordonatele triunghiului in fisierul Coordonate.txt si le citesc intr-un vector puncte[3][3];
            float[][] puncte = new float[3][];
            for (int i = 0; i < linii.Length; i++)
            {
                string[] linie = linii[i].Split(' ');
                puncte[i] = new float[3];
                puncte[i][0] = float.Parse(linie[0]);
                puncte[i][1] = float.Parse(linie[1]);
                puncte[i][2] = float.Parse(linie[2]);
            }


            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(culori[0][0], culori[0][1], culori[0][2]);
            GL.Vertex3(puncte[0][0] + constantaX, puncte[0][1] + constantaY, puncte[0][2]);

            GL.Color3(culori[1][0], culori[1][1], culori[1][2]);
            GL.Vertex3(puncte[1][0] + constantaX, puncte[1][1] + constantaY, puncte[1][2]);

            GL.Color3(culori[2][0], culori[2][1], culori[2][2]);
            GL.Vertex3(puncte[2][0] + constantaX, puncte[2][1] + constantaY, puncte[2][2]);

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
