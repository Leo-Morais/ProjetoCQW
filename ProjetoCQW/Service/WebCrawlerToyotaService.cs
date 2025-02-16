﻿using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ProjetoCQW.DTO;
using ProjetoCQW.Model;
using ProjetoCQW.Repository;
using System;
using Microsoft.EntityFrameworkCore;
using ProjetoCQW.CustomExceptions;

namespace ProjetoCQW.Service
{
    public class WebCrawlerToyotaService : IWebCrawlerToyotaService
    {
        private readonly ConnectionContext _context;
        private readonly IModeloCarroService _modeloCarroService;
        private readonly IModeloSiteDetalheService _modeloSiteDetalheService;

        public WebCrawlerToyotaService(ConnectionContext context, IModeloCarroService modeloCarroService, IModeloSiteDetalheService modeloSiteDetalheService)
        {
            _context = context;
            _modeloCarroService = modeloCarroService;
            _modeloSiteDetalheService = modeloSiteDetalheService;
        }

        public async Task<ModeloCarro> Update(int modeloCarroID, int modeloSiteID)
        {
            if(modeloCarroID < 0)
            {
                throw new IdNotFoundException("modeloCarroID inválido");
            }

            if(modeloSiteID < 0)
            {
                throw new IdNotFoundException("modeloSiteID inválido");
            }


            var modeloCarro = await _context.ModeloCarros.FindAsync(modeloCarroID);
            var modeloSite = await _context.ModeloSiteDetalhes.FindAsync(modeloSiteID);

            var options = new ChromeOptions();
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("headless");
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64)" +
                               "AppleWebKit/537.36 (KHTML, like Gecko)" +
                               "Chrome/87.0.4280.141 Safari/537.36");
            options.AddArgument("--disable-logging");
            options.AddArgument("--log-level=3");

            using (IWebDriver driver = new ChromeDriver(options))
            {
                var crawler = new WebCrawlerDTO
                {
                    Cor = modeloSite.XpathCor,
                    Imagem = modeloSite.XpathImg,
                    Modelo = modeloSite.XpathModelo,
                    Url = modeloSite.UrlSite,
                    Valor = modeloSite.XpathValor,
                    Nome = modeloSite.XpathNome,  
                };

      

                driver.Navigate().GoToUrl(crawler.Url);

                System.Threading.Thread.Sleep(2000);

                var fecharElement = driver.FindElement(By.XPath("//*[@id=\"onetrust-banner-sdk\"]/div/div/div[1]/button"));
                fecharElement.Click();

                var xpathXS = driver.FindElement(By.XPath("//*[@id=\"dynamic-component-0\"]/div[2]/section/div/section[1]/div[2]/div/div/div/div/div[1]/div/div/button"));
                var xpathXL = driver.FindElement(By.XPath("//*[@id=\"dynamic-component-0\"]/div[2]/section/div/section[1]/div[2]/div/div/div/div/div[2]/div/div/button"));
                var xpathXLS = driver.FindElement(By.XPath("//*[@id=\"dynamic-component-0\"]/div[2]/section/div/section[1]/div[2]/div/div/div/div/div[3]/div/div/button"));

                if(modeloCarro.Versao.ToLower() == xpathXS.Text.ToLower()) xpathXS.Click();
                if (modeloCarro.Versao.ToLower() == xpathXL.Text.ToLower()) xpathXL.Click();
                if (modeloCarro.Versao.ToLower() == xpathXLS.Text.ToLower()) xpathXLS.Click();

                var modeloElement = driver.FindElement(By.XPath(crawler.Nome));
                var modeloEle = modeloElement.Text.ToUpper().Trim();
                modeloCarro.Nome = modeloEle;

                var colorElement = driver.FindElement(By.XPath(crawler.Cor));
                var color = colorElement.Text.ToUpper().Trim();
                modeloCarro.Cor = color;

                var valorElement = driver.FindElement(By.XPath(crawler.Valor));
                var valor = valorElement.Text.ToUpper().Trim();
                valor = valor.Replace("R$", "").Trim();
                float valorFinal = float.Parse(valor);
                modeloCarro.Valor = valorFinal;

                var imgElement = driver.FindElement(By.XPath(crawler.Imagem));
                string imgSrc = imgElement.GetAttribute("src");
                modeloCarro.Imagem = imgSrc;

                //var versaoElement = driver.FindElement(By.XPath(crawler.Modelo));
                //var versao = versaoElement.Text.ToUpper().Trim();
                //modeloCarro.Versao = versao;
                modeloCarro.DataAtualizacao = DateTime.Now;
                driver.Close();

                 _context.ModeloCarros.Update(modeloCarro);
                 await _context.SaveChangesAsync();

                return modeloCarro;
            }

            
        }

       
    }

}  

