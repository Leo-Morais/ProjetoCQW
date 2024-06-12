using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace ProjetoCQW.Service
{
        class Program
        {
            static void Main(string[] args)
            {

                String url = "https://www.toyota.com.br/modelos/yaris-hatch";

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
                    driver.Navigate().GoToUrl(url);

                    System.Threading.Thread.Sleep(2000);

                    var fecharElement = driver.FindElement(By.XPath("//*[@id=\"onetrust-banner-sdk\"]/div/div/div[1]/button"));
                    fecharElement.Click();

                    var modeloElement = driver.FindElement(By.XPath("//*[@id=\"dynamic-component-0\"]/div[2]/section/div/section[1]/div[1]/p"));
                    var modelo = modeloElement.Text.ToUpper().Trim();
                    Console.WriteLine(modelo);

                    var colorElement = driver.FindElement(By.XPath("//*[@id=\"dynamic-component-0\"]/div[2]/section/div/section[2]/div/div[1]/div[1]/div[2]/div[2]/span"));
                    var color = colorElement.Text.ToUpper().Trim();
                    Console.WriteLine(color);

                    var valorElement = driver.FindElement(By.XPath("//*[@id=\"dynamic-component-0\"]/div[2]/section/div/section[2]/div/div[1]/div[3]/span[2]"));
                    var valor = valorElement.Text.ToUpper().Trim();
                    Console.WriteLine(valor);

                    var ImgElement = driver.FindElement(By.XPath("//*[@id=\"dynamic-component-4\"]/section/ul/li[1]/div[1]/div/img"));
                    String imgSrc = ImgElement.GetAttribute("src");
                    Console.WriteLine(imgSrc);

                    var button = driver.FindElement(By.XPath("//*[@id=\"dynamic-component-0\"]/div[2]/section/div/section[1]/div[2]/div/div/div/div/div[2]/div/div/button"));
                    //Actions action = new Actions(driver);
                    //action.MoveToElement(button).Perform();
                    button.Click();

                    valorElement = driver.FindElement(By.XPath("//*[@id=\"dynamic-component-0\"]/div[2]/section/div/section[2]/div/div[1]/div[3]/span[2]"));
                    valor = valorElement.Text.ToUpper().Trim();
                    Console.WriteLine(valor);
                }
            }
        }
    }
