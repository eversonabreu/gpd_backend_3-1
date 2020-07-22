using GPD.Backend.Domain.Services.Contracts;
using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using System;
using System.Text;

namespace GPD.Backend.Domain.Reports
{
    public class IndicadoresReport
    {
        private readonly IndicadorResultadosRelatorio indicadorResultados;
        private readonly StringBuilder html;

        public IndicadoresReport(IndicadorResultadosRelatorio indicadorResultados)
        {
            this.indicadorResultados = indicadorResultados;
            html = new StringBuilder();
            GenerateHtml();
        }

        private (Document, string) GetDocumentByPdf()
        {
            string path = System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory());
            string nomeRelatorio = $"relatorio_{new Random().Next(0, 9857425)}_{DateTime.Now:yyyyMMddHHmmssfff}.pdf";
            string pathRelatorio = System.IO.Path.Combine(path, nomeRelatorio);
            var writer = new PdfWriter(pathRelatorio);
            var pdf = new PdfDocument(writer);
            return (new Document(pdf, PageSize.A4), pathRelatorio);
        }

        private void GenerateHtml()
        {
            string stylePages = @"<style> 
                                  @page { 
                                        size: A4; 
                                        margin: 15%; 
                                        margin-bottom: 2cm; 
                                        margin-left: 1cm; 
                                        margin-right: 1cm; 

                                        @top-left { 
                                            content: 'Relatório de indicadores'; 
                                        } 
                                    
                                        @top-right { 
                                            content: '" + indicadorResultados.Periodo + @"'; 
                                            white-space: pre-wrap; 
                                            text-align: center; 
                                            padding: 1.5em; 
                                            position: absolute; 
                                        } 

                                        @bottom-center { 
                                            content: 'Página ' counter(page) ' de ' counter(pages); 
                                            font-size: 12px; 
                                        } 
                                        @bottom-right { 
                                            content: 'Impresso em: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + @"'; 
                                            font-size: 12px; 
                                        } 
                                    } 

                                    table { 
                                      font-family: arial, sans-serif; 
                                      border-collapse: collapse; 
                                      width: auto; 
                                    } 

                                    td, th { 
                                      border: 1px solid #dddddd; 
                                      text-align: left; 
                                      padding: 8px; 
                                      font-weight: 400; 
                                    } 
                                 </style>";
            html.AppendLine($"<!DOCTYPE html><html><head>{stylePages}</head><body>");

            foreach (var projeto in indicadorResultados.Projetos)
            {
                foreach (var cargo in projeto.Cargos)
                {
                    foreach (var usuario in cargo.Usuarios)
                    {
                        var builder = new StringBuilder();
                        foreach (var indicador in usuario.Indicadores)
                        {
                            html.AppendLine($"<table><tr><th>Projeto: <b>{projeto.NomeProjeto}</b></th><th>Id do Projeto: <b>{projeto.IdProjeto}</b></th></tr>");
                            html.AppendLine($"<tr><th>Cargo: <b>{cargo.Cargo}</b></th><th>Usuário: <b>{usuario.Usuario}</b></th></tr>");
                            html.AppendLine($"<tr><th>Indicador: <b>{indicador.NomeIndicador}</b></th><th>Identificador: <b>{indicador.Identificador}</b></th><th>Unidade de Medida: <b>{indicador.UnidadeMedida}</b></th><th>Critério (%): <b>{indicador.ValorPercentualCriterio}</b></th><th>Peso (%): <b>{indicador.ValorPercentualPeso}</b></th></tr>");
                            html.AppendLine("<tr style='background-color: #dddddd'><th></th><th style='color:red'>Lançamentos</th><th></th><th></th><th></th></tr><tr><th style='background-color: #dddddd'></th><th><b>Ano</b></th><th><b>Mês</b></th><th><b>Meta</b></th><th><b>Realizado</b></th></tr>");
                            
                            foreach (var lancamento in indicador.Lancamentos)
                            {
                                html.AppendLine($"<tr><th style='background-color: #dddddd'></th><th>{lancamento.Ano}</th><th>{lancamento.Mes}</th><th>{lancamento.ValorMeta}</th><th>{lancamento.ValorRealizado}</th></tr>");
                            }

                            html.AppendLine("<tr style='background-color: #dddddd'><th></th><th style='color:red'>Resultados do indicador</th><th></th><th></th></tr><tr><th style='background-color: #dddddd'></th><th><b>Meta (calculado)</b></th><th><b>Realizado (calculado)</b></th><th><b>Atingimento (%)</b></th></tr>");
                            html.AppendLine($"<tr><th style='background-color: #dddddd'></th><th>{indicador.ValorMetaCalculado}</th><th>{indicador.ValorRealizadoCalculado}</th><th>{indicador.ValorAtingimento}</th></tr>");
                            html.AppendLine("<tr style='background-color: #dddddd'><th></th><th style='color:red'>Resultados do usuário</th><th></th><th></th></tr><tr><th style='background-color: #dddddd'></th><th><b>Ponderado individual (%)</b></th><th><b>Ponderado corporativo (%)</b></th><th><b>Ponderado final (%)</b></th></tr>");
                            html.AppendLine($"<tr><th style='background-color: #dddddd'></th><th>{usuario.PonderadoInvidual}</th><th>{usuario.PonderadoCorporativo}</th><th>{usuario.PonderadoFinal}</th></tr>");
                            html.AppendLine("</table><br/><br/>");
                        }
                    }
                }
            }

            html.AppendLine("</body></html>");
        }

        public ReportDto GetPdf()
        {
            var document = GetDocumentByPdf();
            string htmlText = html.ToString();
            var pdfDocument = document.Item1.GetPdfDocument();
            HtmlConverter.ConvertToPdf(htmlText, pdfDocument, new ConverterProperties());
            var bytes = System.IO.File.ReadAllBytes(document.Item2);
            System.IO.File.Delete(document.Item2);
            string result = Convert.ToBase64String(bytes);
            return new ReportDto { Base64 = result };
        }
    }
}
