using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkHttp
{
    class sampleHandler:Http.IHttpHandler
    {
        private httpservertest serform;
        private string connections;

        public sampleHandler(httpservertest server)
        {
            this.serform = server;
            this.connections = null;
        }
        public myHandler()
        {
            this.connections = null;
        }
        public bool Process(Http.HttpServer server, Http.HttpRequest req, Http.HttpResponse resp)
         {
            server.RequestSession(req);
            connections = "client get request with ip:" + req.Session.User.ToString() + "\n";
            serform.lbcontent.Text = connections;
            //js response
            if (req.Url == "/js/jquery.min.js")
                httpresponseType.jqueryminjs(resp);
            else if (req.Url == "/js/bootstrap.min.js")
                httpresponseType.bootstrapminjs(resp);
            else if (req.Url == "/js/jqteeditor.js")
                httpresponseType.loadjs(resp, "jqteeditor.js");
            else if (req.Url == "/js/imgload.js")
                httpresponseType.loadjs(resp, "imgload.js");
            else if (req.Url == "/js/dxstudioplayer.js")
                httpresponseType.loadjs(resp, "dxstudioplayer.js");
            else if (req.Url == "/js/jquery.liquid-slider.min.js")
                httpresponseType.loadjs(resp, "jquery.liquid-slider.min.js");
            else if (req.Url == "/js/jquery.touchSwipe.min.js")
                httpresponseType.loadjs(resp, "jquery.touchSwipe.min.js");
            //css response
            else if (req.Url == "/css/bootstrap.min.css")
                httpresponseType.loadcss(resp, "bootstrap.min.css");
            else if (req.Url == "/css/template.css")
                httpresponseType.loadcss(resp, "template.css");
            else if (req.Url == "/css/custome_jqte.css")
                httpresponseType.loadcss(resp, "custome_jqte.css");
            else if (req.Url == "/css/animate.css")
                httpresponseType.loadcss(resp, "animate.css");
            else if (req.Url == "/css/liquid-slider.css")
                httpresponseType.loadcss(resp, "liquid-slider.css");
            //file response
            else if (req.Url == "/videos_pictures/jquery-te.png")
                httpresponseType.loadpicvideo(resp, "jquery-te.png", "videos_pictures");
            else if (req.Url == "/videos_pictures/angrybird")
                httpresponseType.loadswf(resp, "angry-birds.swf", "videos_pictures");
            else if (req.Url == "/videos_pictures/FireBall.dxstudio")
                httpresponseType.link(resp, "FireBall.dxstudio", "videos_pictures");
            //load fonts
            else if (req.Url == "/fonts/glyphicons-halflings-regular.eot")
                httpresponseType.link(resp, "glyphicons-halflings-regular.eot", "fonts");
            else if (req.Url == "/fonts/glyphicons-halflings-regular.svg")
                httpresponseType.link(resp, "glyphicons-halflings-regular.svg", "fonts");
            else if (req.Url == "/fonts/glyphicons-halflings-regular.ttf")
                httpresponseType.link(resp, "glyphicons-halflings-regular.ttf", "fonts");
            else if (req.Url == "/fonts/glyphicons-halflings-regular.woff")
                httpresponseType.link(resp, "glyphicons-halflings-regular.woff", "fonts");
            else if (req.Url == "/fonts/glyphicons-halflings-regular.woff2")
                httpresponseType.link(resp, "glyphicons-halflings-regular.woff2", "fonts");
            //load directx studio
            else if (req.Url == "/FireBall")
                httpresponseType.htmlpage(resp, "FireBall.html", "");
            else if (httpresponseType.form_postto(req) == "/send")
            {
                resp.Content = req.QueryString;
                System.IO.StreamWriter sr = new System.IO.StreamWriter(@"..\..\pages\querystr.txt");
                sr.WriteLine(req.QueryString);
                sr.Close();
            }
            //page response
            else if (req.Url == "/page2")
            {
                string appendelem = "<script>$('#main').html(\"" + "<div class='pg2div'><h1 id='pg2h1'>read from db</h1><b>" + httpresponseType.readDBfiled(DataBase.sqlite, "name") + "</div>" + "\")</script>";
                httpresponseType.htmlpage(resp, "index.html", appendelem, "<div id=\"cont\" class=\"content\">");
            }
            else if (req.Url == "/page3")
                httpresponseType.htmlpage(resp, "index.html", "<script type=\"text/javascript\">" + httpresponseType.Readjs("addjqte.js") + "</script>");
            else if (req.Url == "/page4")
                if (req.QueryString == "user:farzad")
                    httpresponseType.EXNONquery(resp, DataBase.sqlite, "Data Source=HTMsource.db;Version=3;", "insert into page(content) values('hello new world')");
                else
                    resp.Content = "not true user";
            else if (req.Url == "/videos_pictures/cinqueterre.jpg")
                httpresponseType.loadpicvideo(resp, "cinqueterre.jpg", "videos_pictures");
            else if (req.Url == "/page5")
                httpresponseType.htmlpage(resp, "htmlslider.html", "");
            else
            {
                httpresponseType.htmlpage(resp, "index.html", "<script>$('#pag').click(function(){$('#ld').show();})</script>");
                //httpresponseType.defaultresponse(req, resp);
            }
            return true;
        }
    }
}
