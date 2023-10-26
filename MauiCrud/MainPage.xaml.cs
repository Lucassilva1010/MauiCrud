using SQLite; //Importando o pacote do SQLite

namespace MauiCrud
{
    public partial class MainPage : ContentPage
    {
        //Criando configuração para o banco de dados
        string _dbPath; //Caminho do banco de dados 
        SQLiteConnection conexao;// Conexão com o banco de dados Sqlite

        public MainPage()
        {
            InitializeComponent();//Inicializa os componentes
        }

        private void CriarBancoDadosBtn_Clicked(object sender, EventArgs e)
        {
            //Evento que irá criar meu banco de dados

            //definição do caminho para o banco de dados
            _dbPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "sites.db1");

            //criaando o banco
            conexao = new SQLiteConnection(_dbPath); //cria uma conexão a partir de um caminho que foi passado pelo pranmetro
            conexao.CreateTable<Site>();//Criação da tabela. ---> Tabela que é representada pela classe SITE.cs
            OperacoesVsl.IsVisible = true;//Isso vai fazer ficar visievel quando houver dados ---> Com isso cada botão so vai aparecer se tiver alguma ação a realizar
           
        }

        private void InserirBtn_Clicked(object sender, EventArgs e)
        {
            //Inserir Dados no Banco de dados 
            Site site = new Site();
            site.Endereco = ValorEntry.Text;// Busca a propriedade e passa como valor para ela, o que digitamos na tela, com o Entry
            conexao.Insert(site);//Guarda No banco de dados
            ListarDados();
            ValorEntry.Text = "";
            IdEntry.Text = "";

        }
        private void ListarBtn_Clicked(object sender, EventArgs e)
        {
            //Listar dados do Banco
            ListarDados();
        }

        public void ListarDados()
        {
            List<Site> sites = conexao.Table<Site>().ToList();//preeenche a nova lista com os dados que tem dentro do banco
            ListaCv.ItemsSource = sites;
        }
        private void ExcluirBtn_Clicked_1(object sender, EventArgs e)
        {
            //Excluir dados do Banco 
            App.Current.MainPage.DisplayAlert("Exluir", "Tem certeza que deseja Excluir? ", "ok");

            int id = int.Parse(IdEntry.Text);
            conexao.Delete<Site>(id);
            ListarDados();
            IdEntry.Text = "";

        }

        private void AlterarBtn_Clicked(object sender, EventArgs e)
        {
            //Alterar Dados No banco 
            Site site = new Site();
            site.Id = int.Parse(IdEntry.Text);
            site.Endereco = ValorEntry.Text;

            conexao.Update(site);
            ListarDados();
        }

        private void LocalizarBtn_Clicked(object sender, EventArgs e)
        {
            //localizar Dados no banco

            int id = int.Parse(IdEntry.Text);
            var banco =  from b in conexao.Table<Site>() where b.Id == id
                         select b;//Buscar um valor do tipo ID dentro da tabela, aonde ela possui aquele mesmo ID que foi passado pelo usuario
            Site site = banco.First();//retonar o valor do banco selecionado na Query
            IdEntry.Text = site.Id.ToString();// retornando para label o mesmo valor que existe no banco pelo ID
            ValorEntry.Text = site.Endereco;

        }
       
        
    }
}