namespace RageAudioTool.Rage_Wrappers.DatFile.XML
{
    class ResourceXMLReader5
    {
        private string filename;

        public ResourceXMLReader5(string fileName)
        {
            filename = fileName;
        }

     /*   public RageAudioMetadata5 ReadData()
        {
            IList<string> strings = new List<string>();

            IList<audSoundBase> dataItems = new List<audSoundBase>();

            IList<IRageDatItemBase> hashItems = new List<IRageDatItemBase>();

            IList<IRageDatItemBase> hashItems1 = new List<IRageDatItemBase>();

            RageAudioMetadata5 md = new RageAudioMetadata5();

            XDocument xml = XDocument.Load(filename);

            foreach (var node in xml.Descendants("stringTable"))
            {
                foreach (var subNode in node.Descendants())
                {
                    strings.Add(subNode.Value);
                }

                md.StringTable = strings.ToArray();

                break;
            }

            foreach (var node in xml.Descendants("audioEntries"))
            {
                foreach (var subNode in node.Descendants("audioEntry"))
                {
                    var type = Enum.Parse(typeof(dat54_audMetadataTypes), subNode.Attribute("type").Value);

                    var subNodes = subNode.Descendants();

                    string name = subNodes.FirstOrDefault(x => x.Name == "name").Value;

                    string data = subNodes.FirstOrDefault(x => x.Name == "data").Value;

                    dataItems.Add(new audSoundBase(name));             
                }

                md.DataItems = dataItems.ToArray();

                break;
            }

            foreach (var node in xml.Descendants("hashItems"))
            {
                foreach (var subNode in node.Descendants("item"))
                {
                    hashItems.Add(new DatTypeBase<uint>(Convert.ToUInt32(subNode.Value)));
                }

                md.HashItems = hashItems.ToArray();

                break;

            }

            foreach (var node in xml.Descendants("hashItems1"))
            {
                foreach (var subNode in node.Descendants("item"))
                {
                    hashItems1.Add(new DatTypeBase<uint>(Convert.ToUInt32(subNode.Value)));
                }

                md.HashItems1 = hashItems1.ToArray();

                break;
            }
    
            using (var reader = XmlReader.Create(filename))
            {
                if (reader.ReadToDescendant("audioEntries"))
                {
                    while (reader.ReadToNextSibling("audioEntry"))
                    {
                        var type = Enum.Parse(typeof(dat54_audMetadataTypes), reader.GetAttribute("type"));

                        if (reader.ReadToDescendant("name"))
                        {
                            string name = reader.ReadElementContentAsString();

                            if (reader.ReadToDescendant("data"))
                            {
                                string data = reader.ReadContentAsString();

                                dataItems.Add(new DataFileObject<string>((int)type, name, data));

                                md.DataItems = dataItems.ToArray();
                            }
                        }
                    }
                }

                if (reader.ReadToDescendant("hashItems"))
                {
                    while (reader.ReadToNextSibling("item"))
                    {
                        hashItems.Add(new DatTypeBase<uint>((uint)reader.ReadElementContentAsInt()));

                        md.HashItems = hashItems.ToArray();
                    }
                }


                if (reader.ReadToDescendant("hashItems1"))
                {
                    while (reader.ReadToNextSibling("item"))
                    {
                        hashItems1.Add(new DatTypeBase<uint>((uint)reader.ReadElementContentAsInt()));

                        md.HashItems = hashItems1.ToArray();
                    }
                }
            }

            return md;
        }*/
    }
}
