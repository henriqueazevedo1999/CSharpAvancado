using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Carro unoComEscada = new Carro("Fiat", "Uno Escada - NET", 340, false);
            Carro fiestaComArLigado = new Carro("Ford", "Fiesta 1.0", 62, false);

            unoComEscada.Acelerar();
            unoComEscada.Acelerar();

            fiestaComArLigado.Acelerar();
            fiestaComArLigado.Acelerar();
        }

    }

    class Carro
    {
        public Carro(string marca, string modelo, double cv, bool temAbs)
        {
            this.Marca = marca;
            this.Modelo = modelo;
            this.CV = cv;
            this.TemAbs = temAbs;
        }

        private double velocidade;

        public void Acelerar()
        {
            if (this.CV < 100)
                velocidade += 30;
            else if (this.CV < 200)
                velocidade += 40;
            else
                velocidade += 50;
        }

        public void Frear()
        {
            if (TemAbs)
                this.velocidade -= 40;
            else
                this.velocidade -= 20;

            if (this.velocidade < 0)
                this.velocidade = 0;
        }

        public double LerVelocidade(bool deveMostrarKM)
        {
            return deveMostrarKM ? velocidade : velocidade / 1.5;
        }

        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public double CV { get; private set; }
        public bool TemAbs { get; private set; }
    }
}
