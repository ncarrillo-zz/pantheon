using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using HtmlAgilityPack;
using Pantheon.Core;
using Pantheon.Compiler.Core;
using Pantheon.Compiler.Core.Generators;

namespace XamlTester
{
    class Program
    {
        static void Main(string[] args)
        {
            // Testbed XAML.
            var s = @"<Page Title='Hello World from XAML' xmlns='Pantheon/v1'>
	                    <Page.Resources>
                            <Script Name='Knockout' Type='JavaScript' Uri='Knockout.js' />
                            
                            <Style Name='ButtonStyle' For='Button'>
                                <Set Property='Background'>#666666</Set>
                                <Set Property='Foreground'>#FFFFFF</Set>
                                <Set Property='Margin'>12</Set>
                            </Style>

                            <Style Name='ButtonStyle2' For='Button'>
                                <Set Property='Background'>#444444</Set>
                                <Set Property='Foreground'>#FFFFFF</Set>
                                <Set Property='Margin'>12</Set>
                                <Set Property='Height'>200</Set>
                            </Style>

                            <Style Name='StackPanelStyle' For='StackPanel'>
                                <Set Property='Orientation' To='Horizontal' />
                            </Style>

                            <Style Name='StackPanelStyle2' For='StackPanel'>
                                <Set Property='Orientation' To='Vertical' />
                            </Style>

                            <Source Name='VmSource' ViewModel='knockoutVm' />
	                    </Page.Resources>

                        <StackPanel Style='StackPanelStyle'>
                            <Button Style='ButtonStyle' />
                            <Button Style='ButtonStyle'>
                                <StackPanel Style='StackPanelStyle2'>
                                    <Button Style='ButtonStyle2' />
                                    <Button Style='ButtonStyle2' />
                                    <Button Style='ButtonStyle2' />
                                </StackPanel>
                            </Button>
                            <Button Style='ButtonStyle' />
                        </StackPanel>
                      </Page>";

            var pantheonC = new Compiler();
            pantheonC.Parse(s);

            Console.WriteLine("Finished compiling.");
            Console.ReadLine();
        }
    }

    /// <summary>
    /// TODO: Flesh this out. Barebones at the moment.
    /// </summary>
    public class Compiler
    {
        public void Parse(string input, string[] args = null)
        {
            var xxr = new XamlXmlReader(new StringReader(input), new XamlSchemaContext());
            var graphReader = new XamlObjectWriter(xxr.SchemaContext);

            while (xxr.Read())
                graphReader.WriteNode(xxr);

            var page = (Page)graphReader.Result;

            // Map our generators
            var g = new Generator();
            g.Map<Page, PageGeneratorBlock>();
            g.Map<Button, ButtonGeneratorBlock>();
            g.Map<StackPanel, StackPanelGeneratorBlock>();

            var doc = new HtmlDocument();
            var html = doc.CreateElement("html");

            g.Generate(html, page);
            // HTML5 Doc type
            doc.DocumentNode.AppendChild(doc.CreateComment("<!DOCTYPE html>"));
            doc.DocumentNode.AppendChild(html);

            doc.Save("test.htm");

            var cssContents = g.GenerateStyles(page);
            File.WriteAllText("XamlCore.css", cssContents);
        }
    }
}
