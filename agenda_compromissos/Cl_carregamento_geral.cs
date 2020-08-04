﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

//using System.Text.Json;
//using System.Text.Json.Serialization;
namespace agenda_compromissos
{
   
    public class Cl_carregamento_geral
    {
        
       
        //carregar a versao e informações de leitura e gravação de ficheiro stream reader e writer
        public static string versao = "1.0.0";

        //criar uma list publica e estatica do tipo a classe registroAgenda
        //para usar depois para escrever e gravar cada registro de compromisso
        //mais pra frente essa porção de registro sera colocada em uma tabela
        public static List<registroAgenda> LISTA_REG;  //lista invisivel que guardara conjunto de registros da agenda
        private static JsonSerializer JsonString;


        //===============================================================
        //metodo para ler e escrever os dados dentro de uma lista
        public static void construirLista() //esse metodo sera chamado no form principal apartir dessa classe ira carregar as informacoes
        {
            //verifica se o ficheiro exites
            string pastaDoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string nomeFicheiro = pastaDoc + @"\dadoscompromissos.txt";
            string meu;
            LISTA_REG = new List<registroAgenda>();
            //criar a isntancia da lista aqui fora pq se for criar dentro ao carregar os contatos como nao existe ele daria erro
           
            //verificar se ja existe o file /documento
            //se existe ele lê/ carrega
            //se nao (!existe) cria ou escreve stream writer
            if (File.Exists(nomeFicheiro))
            //se existe ele cria uma instancia da classe stream eader ( que le o file) 
            {
                StreamReader ficheiro = new StreamReader(nomeFicheiro, Encoding.Default);//definir  reconhecimento de acentos

                //criar uma instacia da list<> dos registros para ler cada um gravado


                //carregar todos compromissos do ficheiro
                //enquanto nao chegar ao fim do stream do ficheiro
                while (!ficheiro.EndOfStream)
                {//cada informacao vai carregar uma linha

                    //carrega compromisso em uma variavel
                    string compromisso = ficheiro.ReadLine();//uma linha do ficheiro
                                                             //carrega horario 
                    string horario = ficheiro.ReadLine();//proxima linha do ficheiro
                                                         //carrega dia da semana
                    string DiaDaSemana = ficheiro.ReadLine();//proxima linha
                                                             //carrega duracao
                    string duracao = ficheiro.ReadLine();//e le a ultima linha desse compromisso
                                                         //apos essas 4 informações ele vai fechar e criar um registro na lsita
                                                         //agora acrescentar toda essa informacao em um registro ,esse registro depois vai para dentro de uma LISTA_REG

                    // a classe de registro ja existe registro agenda basta instanciar e para cada propriedade dela, linka a string com informação


                    registroAgenda novoRegistro = new registroAgenda();
                    novoRegistro.compromisso = compromisso; //vai para um registro que depois cada registro vai para uma lista
                    novoRegistro.horario = horario;
                    novoRegistro.DiaDaSemana = DiaDaSemana;
                    novoRegistro.duracao = duracao;
                    // e agora que tem todas as informacoes de um registro ele será ADD a lista ,a lista e do tipo de registroAgenda
                    LISTA_REG.Add(novoRegistro);
                    //enquanto a lista nao acabar ele vai lendo o proximo item(novoRegistro) da lista_reg ()..ate acabar
                    //tentar colocar aqui no final para escrever em json essa list
                    //agora serializa a cada  novo registro
                    //using Nwetonsoft.Json

                    // string meujson = JsonConvert.SerializeObject(novoRegistro, Formatting.Indented);
                    //////e agora como serializo?
                  
                    //JsonString = JsonSerializer.CreateDefault();
                    //JsonString.Serialize(jsonWriter, novoRegistro);

                    //File.WriteAllText(nomeFicheiro, JsonString);
    }
                ficheiro.Dispose();
              
                
                
            }
            
       
           

        }
      
        
        //===============================================================
        //gravar novo registro na lsita
        public static void gravarNovoReg(string comp, string hora, string diasema, string dura)
        {//gravar um novo registro(na lista e no doc ficheiro)
         //gravar na lista
            LISTA_REG.Add(new registroAgenda() { compromisso = comp, horario = hora, DiaDaSemana = diasema, duracao = dura });
            //passa todos dados de  registro como se fosse novo registroAgenda que é o que contem dentro dessa lista invisivel LIST_REG
            //poderia ter ciado instancia de registro agenda acessar propriedades dela para receber os valores passados mas dessa forma é reduzida 

            //atualiza o ficheiro doc chamando metodo para graavar no doc
            gravaFicheiro();

        }
        //===============================================================
        public static void gravaFicheiro()
        {
            string pastaDoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string nomeFicheiro = pastaDoc + @"\dadoscompromissos.txt";
            //duplicar info de caminho para escrever o ficheiro
            StreamWriter ficheiro = new StreamWriter(nomeFicheiro, false, Encoding.Default);
            // isntancia da classe stream writer ("caminyho",vai gravar por cima do que existe ,nao vai apenas add, acnetucao)
            foreach (registroAgenda registro in LISTA_REG)
            {
                //para cada registro da lista vai gravar 4 linhas
                ficheiro.WriteLine(registro.compromisso);
                ficheiro.WriteLine(registro.horario);
                ficheiro.WriteLine(registro.DiaDaSemana);
                ficheiro.WriteLine(registro.duracao);
            }
            ficheiro.Dispose();
            //ao add o novo contato na lista ele pega essa lista com esse e outros registros ja gravados e escreve por cima no doc
        }
    }

}
