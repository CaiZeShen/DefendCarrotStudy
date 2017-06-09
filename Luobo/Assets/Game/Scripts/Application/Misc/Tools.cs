using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using UnityEngine.UI;
using System.Xml.Linq;

// ****************************************************************
// 功能：工具类
// 创建：蔡泽深
// 时间：2017/06/01
// 修改内容：										修改者姓名：
// ****************************************************************

public static class Tools {

    // 读取关卡列表
    public static List<FileInfo> GetLevelFiles() {
        // 获取目录下所有文件
        string[] files = Directory.GetFiles(Consts.LevelDir, "*.xml");
        List<FileInfo> fileInfos = new List<FileInfo>();

        for (int i = 0; i < files.Length; i++) {
            FileInfo fileInfo = new FileInfo(files[i]);
            fileInfos.Add(fileInfo);
        }

        return fileInfos;
    }

    /// <summary>
    /// 填充Level类数据
    /// </summary>
    /// <param name="fileFullName">包含路径</param>
    /// <param name="level"></param>
    public static void FillLevel(string fileFullName, ref Level level) {
        FileInfo fileInfo = new FileInfo(fileFullName);
        StreamReader sr = new StreamReader(fileInfo.OpenRead(), Encoding.UTF8);

        XmlDocument xml = new XmlDocument();
        xml.Load(sr);

        level.name = xml.SelectSingleNode("/Level/Name").InnerText;
        level.cardImage = xml.SelectSingleNode("/Level/CardImage").InnerText;
        level.background = xml.SelectSingleNode("/Level/Background").InnerText;
        level.road = xml.SelectSingleNode("/Level/Road").InnerText;
        level.initSocre = int.Parse(xml.SelectSingleNode("/Level/InitScore").InnerText);

        XmlNodeList nodes;
        nodes = xml.SelectNodes("/Level/Holder/Point");
        for (int i = 0; i < nodes.Count; i++) {
            XmlNode node = nodes[i];
            Point point = new Point(
                int.Parse(node.Attributes["X"].Value),
                int.Parse(node.Attributes["Y"].Value)
            );
            level.holders.Add(point);
        }

        nodes = xml.SelectNodes("/Level/Path/Point");
        for (int i = 0; i < nodes.Count; i++) {
            XmlNode node = nodes[i];
            Point point = new Point(
                int.Parse(node.Attributes["X"].Value),
                int.Parse(node.Attributes["Y"].Value)
            );
            level.roadPoints.Add(point);
        }

        nodes = xml.SelectNodes("/Level/Rounds/Round");
        for (int i = 0; i < nodes.Count; i++) {
            XmlNode node = nodes[i];
            Round round = new Round(
                int.Parse(node.Attributes["Monster"].Value),
                int.Parse(node.Attributes["Count"].Value)
            );
            level.rounds.Add(round);
        }

        sr.Close();
        sr.Dispose();
    }

    // 保存关卡(第一种方法: 字符串写入文本)
    public static void SaveLevle(string fileName, Level level) {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        sb.AppendLine("<Level>");

        sb.AppendLine(string.Format("<Name>{0}</Name>", level.name));
        sb.AppendLine(string.Format("<CardImage>{0}</CardImage>", level.cardImage));
        sb.AppendLine(string.Format("<Background>{0}</Background>", level.background));
        sb.AppendLine(string.Format("<Road>{0}</Road>", level.road));
        sb.AppendLine(string.Format("<InitScore>{0}</InitScore>", level.initSocre));

        sb.AppendLine("<Holder>");
        for (int i = 0; i < level.holders.Count; i++) {
            sb.AppendLine(string.Format("<Point X=\"{0}\" Y=\"{1}\"/>", level.holders[i].x, level.holders[i].y));
        }
        sb.AppendLine("</Holder>");

        sb.AppendLine("<Path>");
        for (int i = 0; i < level.roadPoints.Count; i++) {
            sb.AppendLine(string.Format("<Point X=\"{0}\" Y=\"{1}\"/>", level.roadPoints[i].x, level.roadPoints[i].y));
        }
        sb.AppendLine("</Path>");

        sb.AppendLine("<Rounds>");
        for (int i = 0; i < level.rounds.Count; i++) {
            sb.AppendLine(string.Format("<Round Monster=\"{0}\" Count=\"{1}\"/>", level.rounds[i].monster, level.rounds[i].count));
        }
        sb.AppendLine("</Rounds>");

        sb.AppendLine("</Level>");

        // xml格式化设置
        XmlWriterSettings setting= new XmlWriterSettings();
        setting.Indent = true;
        setting.IndentChars = "\t";
        setting.ConformanceLevel = ConformanceLevel.Auto;
        setting.OmitXmlDeclaration = false;

        XmlWriter xw = XmlWriter.Create(fileName, setting);
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(sb.ToString());
        doc.WriteTo(xw);

        xw.Flush();
        xw.Close();
    }

    // 保存关卡(第二种方法: XmlDocument类)
    public static void SaveLevle2(string fileName, Level level) {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));
        XmlElement root = xmlDoc.CreateElement("Level");
        xmlDoc.AppendChild(root);

        XmlElement name = xmlDoc.CreateElement("Name");
        name.InnerText = level.name;

        XmlElement cardImage = xmlDoc.CreateElement("CardImage");
        cardImage.InnerText = level.cardImage;

        XmlElement background = xmlDoc.CreateElement("Background");
        background.InnerText = level.background;

        XmlElement road = xmlDoc.CreateElement("Road");
        road.InnerText = level.road;

        XmlElement initScore = xmlDoc.CreateElement("InitScore");
        initScore.InnerText = level.initSocre.ToString();

        root.AppendChild(name);
        root.AppendChild(cardImage);
        root.AppendChild(background);
        root.AppendChild(road);
        root.AppendChild(initScore);

        XmlElement holder = xmlDoc.CreateElement("Holder");
        for (int i = 0; i < level.holders.Count; i++) {
            XmlElement point = xmlDoc.CreateElement("Point");
            point.SetAttribute("X", level.holders[i].x.ToString());
            point.SetAttribute("Y", level.holders[i].y.ToString());
            holder.AppendChild(point);
        }

        XmlElement path = xmlDoc.CreateElement("Path");
        for (int i = 0; i < level.roadPoints.Count; i++) {
            XmlElement point = xmlDoc.CreateElement("Point");
            point.SetAttribute("X", level.roadPoints[i].x.ToString());
            point.SetAttribute("Y", level.roadPoints[i].y.ToString());
            path.AppendChild(point);
        }

        XmlElement rounds = xmlDoc.CreateElement("Rounds");
        for (int i = 0; i < level.rounds.Count; i++) {
            XmlElement round = xmlDoc.CreateElement("Round");
            round.SetAttribute("Monster", level.rounds[i].monster.ToString());
            round.SetAttribute("Count", level.rounds[i].count.ToString());
            rounds.AppendChild(round);
        }

        root.AppendChild(holder);
        root.AppendChild(path);
        root.AppendChild(rounds);

        xmlDoc.Save(fileName);
    }

    // 保存关卡(第三种方法: LINQ to XML)
    public static void SaveLevle3(string fileName, Level level) {
        XElement[] holderChilds= new XElement[level.holders.Count];
        for (int i = 0; i < level.holders.Count; i++) {
            holderChilds[i] = new XElement("Point",
                new XAttribute("X", level.holders[i].x),
                new XAttribute("Y", level.holders[i].y)
            );
        }

        XElement[] pathChilds = new XElement[level.roadPoints.Count];
        for (int i = 0; i < level.roadPoints.Count; i++) {
            pathChilds[i] = new XElement("Point",
                new XAttribute("X", level.roadPoints[i].x),
                new XAttribute("Y", level.roadPoints[i].y)
            );
        }

        XElement[] roundsChilds = new XElement[level.rounds.Count];
        for (int i = 0; i < level.rounds.Count; i++) {
            roundsChilds[i] = new XElement("Point",
                new XAttribute("Monster", level.rounds[i].monster),
                new XAttribute("Count", level.rounds[i].count)
            );
        }

        var doc = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            new XElement("Level",
                new XElement("Name",level.name),
                new XElement("CardImage", level.cardImage),
                new XElement("Background", level.background),
                new XElement("Road", level.road),
                new XElement("InitScore",level.initSocre),
                new XElement("Holder", holderChilds),
                new XElement("Path", pathChilds),
                new XElement("Rounds", roundsChilds)
            )
        );

        doc.Save(fileName);
    }

    // 加载图片
    public static IEnumerator LoadImage(string url, SpriteRenderer render) {
        WWW www = new WWW(url);
        yield return www;

        Texture2D texture = www.texture;
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        render.sprite = sp;
    }

    public static IEnumerator LoadImage(string url, Image image) {
        WWW www = new WWW(url);
        yield return www;

        Texture2D texture = www.texture;
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        image.sprite = sp;
    }
}

