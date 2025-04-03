using MauiApp1TempoAgora.Services;
using MauiApp1TempoAgora.Models;

namespace MauiApp1TempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                        $"Longitude: {t.lon} \n" +
                                        $"Nascer do Sol: {t.sunrise} \n" +
                                        $"Por do Sol: {t.sunset} \n" +
                                        $"Temp Máx: {t.temp_max} \n" +
                                        $"Temp Min: {t.temp_min} \n" +                                         
                                        $"Descrição: {t.description} \n" + // acrescentado descrição.
                                        $"Veloc. do vento: {t.speed} \n" + // acrescentado velocidade vento.
                                        $"Visibilidade: {t.visibility}"; // acrescentado visibilidade.


                        lbl_res.Text = dados_previsao;

                    }
                    else
                    {
                        // mensagem específica quando o nome da cidade não for encontrado.
                        lbl_res.Text = $"Sem dados de Previsão para: {txt_cidade.Text}";
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Caso haja problemas com a conexão de rede (sem internet)
                await DisplayAlert("Erro de Conexão", "Parece que você está sem conexão com a internet." +
                    " Verifique sua rede e tente novamente.", "Ok");
            }
           
            catch (Exception ex)
            {
                // Caso haja outros tipos de exceção inesperada
                await DisplayAlert("Ops", ex.Message, "Ok");                           
            }

        }
    }

}
