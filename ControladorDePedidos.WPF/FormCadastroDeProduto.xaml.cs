﻿using ControladorDePedidos.Model;
using ControladorDePedidos.Repositorio;
using System.Windows;

namespace ControladorDePedidos.WPF
{
    public partial class FormCadastroDeProduto : Window
    {
        RepositorioFornecedor repositorioFornecedor;
        RepositorioMarca repositorioMarca;
        RepositorioProduto repositorioProduto;

        public FormCadastroDeProduto()
        {
            repositorioMarca = new RepositorioMarca();
            repositorioFornecedor = new RepositorioFornecedor();
            repositorioProduto = new RepositorioProduto();
            InitializeComponent();
            this.DataContext = new Produto();
        }

        public FormCadastroDeProduto(Produto produto)
        {
            repositorioMarca = new RepositorioMarca();
            repositorioFornecedor = new RepositorioFornecedor();
            repositorioProduto = new RepositorioProduto();
            InitializeComponent();
            this.DataContext = produto;
            cmbMarcas.SelectedValue = produto.Marca.Codigo;
            cmbFornecedor.SelectedValue = produto.Fornecedor.Codigo;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var marcas = repositorioMarca.Liste();
            cmbMarcas.DataContext = marcas;

            var fornecedores = repositorioFornecedor.Liste();
            cmbFornecedor.DataContext = fornecedores;
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var produto = (Produto)this.DataContext;
            if (cmbMarcas.SelectedItem == null)
            {
                MessageBox.Show("Selecione uma marca");
                return;
            }
            else
            {
                produto.Marca = (Marca)cmbMarcas.SelectedItem;
            }

            if (cmbFornecedor.SelectedItem == null)
            {
                MessageBox.Show("Selecione uma fornecedor");
                return;
            }
            else
            {
                produto.Fornecedor = (Fornecedor)cmbFornecedor.SelectedItem;
            }

            if (produto.Codigo == 0)
            {
                repositorioProduto.Adicione(produto);
                // cadastro
            }
            else
            {
                repositorioProduto.Atualize(produto);
                // atualização
            }

            this.Close();
        }
    }
}
