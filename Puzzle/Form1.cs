using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class Puzzle : Form
    {
        private int[,] matriz = new int[4, 4];
        int[,] comparacion = { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 0} };
        int[,] trampa = { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 0, 15 } };
        int raro = 0;
        public Puzzle()
        {
            InitializeComponent();
            RecargarMatriz();
            lblPasos.Text = ""+raro;
        } 
        private void RecargarMatriz()
        {
            llenarMatriz();
            Repintar();
        }
        private void Repintar()
        {
            btn1.Text = matriz[0, 0] == 0 ? "" : matriz[0, 0].ToString();
            btn2.Text = matriz[0, 1] == 0 ? "" : matriz[0, 1].ToString();
            btn3.Text = matriz[0, 2] == 0 ? "" : matriz[0, 2].ToString();
            btn4.Text = matriz[0, 3] == 0 ? "" : matriz[0, 3].ToString();
            btn5.Text = matriz[1, 0] == 0 ? "" : matriz[1, 0].ToString();
            btn6.Text = matriz[1, 1] == 0 ? "" : matriz[1, 1].ToString();
            btn7.Text = matriz[1, 2] == 0 ? "" : matriz[1, 2].ToString();
            btn8.Text = matriz[1, 3] == 0 ? "" : matriz[1, 3].ToString();
            btn9.Text = matriz[2, 0] == 0 ? "" : matriz[2, 0].ToString();
            btn10.Text = matriz[2, 1] == 0 ? "" : matriz[2, 1].ToString();
            btn11.Text = matriz[2, 2] == 0 ? "" : matriz[2, 2].ToString();
            btn12.Text = matriz[2, 3] == 0 ? "" : matriz[2, 3].ToString();
            btn13.Text = matriz[3, 0] == 0 ? "" : matriz[3, 0].ToString();
            btn14.Text = matriz[3, 1] == 0 ? "" : matriz[3, 1].ToString();
            btn15.Text = matriz[3, 2] == 0 ? "" : matriz[3, 2].ToString();
            btn16.Text = matriz[3, 3] == 0 ? "" : matriz[3, 3].ToString();
        }
        private void llenarMatriz()
        {            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int n;
                    bool continuar = false;
                    do
                    {   
                        n = obtenerNumero();
                        continuar = isExiste(n);
                    } while (continuar);
                    matriz[i, j] = n;
                }
            }
            for (int j = 0; j < 3; j++)
            {
                matriz[3, j] = obtenerNumeroFaltante(0);
            }
        }
        private int obtenerNumeroFaltante(int n)
        {
            if (isExiste(n))
            {
                n++;
                n = obtenerNumeroFaltante(n);
            }
            return n;
        }
        private int obtenerNumero()
        {
            return new Random().Next(0, 16);
        }
        private bool isExiste(int num)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if(matriz[i, j] == num)
                    {
                        return true;
                    }                    
                }
            }
            return false;
        }
        private void isGanador()
        {
            int aciertos = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matriz[i, j] == comparacion[i,j])
                    {
                        aciertos++;
                    }
                }
            }
            if(aciertos == 16)
            {
                raro = -1;
                MessageBox.Show("felicidades ganaste la partida","Ganaste");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            raro = 0;
            lblPasos.Text = "" + raro;
            matriz = new int[4, 4];
            RecargarMatriz();
        }
        private int[,] posiciones = new int[4, 2];
        private void identificarTipo(int x, int y)
        {
            int[] pos = obtenerPosicionCero();
            if (pos[0]==x && pos[1]==y)
            {
                return;
            }
            posiciones[0,0] = pos[0];
            posiciones[0,1] = pos[1]-1;
            posiciones[1,0] = pos[0];
            posiciones[1,1] = pos[1]+1;
            posiciones[2, 0] = pos[0]-1;
            posiciones[2, 1] = pos[1];
            posiciones[3, 0] = pos[0]+1;
            posiciones[3, 1] = pos[1];
            if(validarPosicion(x, y))
            {
                int aux;
                aux = matriz[pos[0], pos[1]];
                matriz[pos[0], pos[1]] =  matriz[x, y];
                matriz[x, y] = aux;
                Repintar();
                isGanador();
                raro = raro + 1;
                lblPasos.Text = "" + raro;
            }
        }
        private bool validarPosicion(int x, int y)
        {
            for (int i = 0; i < 4; i++)
            {
                if (posiciones[i, 0] == x && posiciones[i, 1] == y)
                {
                    return true;
                }
            }
            return false;
        }
        private int[] obtenerPosicionCero()
        {
            int[] pos = { 0, 0 };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matriz[i, j] == 0)
                    {
                        pos[0] = i;
                        pos[1] = j;
                    }
                }
            }
            return pos;
        }

        private void btn1_Click(object sender, EventArgs e){identificarTipo(0, 0);}
        private void btn2_Click(object sender, EventArgs e){identificarTipo(0, 1);}
        private void btn3_Click(object sender, EventArgs e){identificarTipo(0, 2);}
        private void btn4_Click(object sender, EventArgs e){identificarTipo(0, 3);}
        private void btn5_Click(object sender, EventArgs e){identificarTipo(1, 0);}
        private void btn6_Click(object sender, EventArgs e){identificarTipo(1, 1);}
        private void btn7_Click(object sender, EventArgs e){identificarTipo(1, 2);}
        private void btn8_Click(object sender, EventArgs e){identificarTipo(1, 3);}
        private void btn9_Click(object sender, EventArgs e){identificarTipo(2, 0);}
        private void btn10_Click(object sender, EventArgs e){identificarTipo(2, 1);}
        private void btn11_Click(object sender, EventArgs e){identificarTipo(2, 2);}
        private void btn12_Click(object sender, EventArgs e){identificarTipo(2, 3);}
        private void btn13_Click(object sender, EventArgs e) { identificarTipo(3, 0); }
        private void btn14_Click(object sender, EventArgs e) { identificarTipo(3, 1); }
        private void btn15_Click(object sender, EventArgs e) { identificarTipo(3, 2); }
        private void btn16_Click(object sender, EventArgs e) { identificarTipo(3, 3); }

        private void button2_Click(object sender, EventArgs e)
        {
            matriz = trampa;
            Repintar();
        }

    }
}
