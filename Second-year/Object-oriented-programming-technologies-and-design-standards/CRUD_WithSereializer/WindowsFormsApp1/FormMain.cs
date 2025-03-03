using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace CRUD
{
    public partial class FormMain : Form
    {       
        public FormMain()
        {
            InitializeComponent();
        }
        public ApplicationDataContext applicationDataContext;    
        private void FormMain_Load(object sender, EventArgs e)
        {
            applicationDataContext = new ApplicationDataContext()
            {
                Objects = new List<BaseObject>(),
                ComboBoxObjects = ComboBoxObjects,
            };
            applicationDataContext.ObjectCreatedEvent += applicationDataContext.AddObjectToList;
            applicationDataContext.ObjectCreatedEvent += applicationDataContext.AddObjectToComboBox;
            applicationDataContext.ObjectDeletedEvent += applicationDataContext.DeleteObjectFromList;
            applicationDataContext.ObjectDeletedEvent += applicationDataContext.DeleteObjectFromComboBox;

            IFactory[] ArrayOfFactories = new IFactory[] {
                new FlattopFactory("Авианосец"),
                new WarPlaneFactory("Военный самолет"),
                new PilotFactory("Пилот"),
            };
            ComboBoxCreate.Items.AddRange(ArrayOfFactories);

            CreateObject createObject = new CreateObject();
            createObject.CreateSomeObjects(applicationDataContext);

            ISerializerFactory[] ArrayOfSerializers = new ISerializerFactory[] {
                new JsonFactory("Json"),
                new BinaryFactory("Бинарная"),
                new ClientFactory("Клиентская"),
            };
            ComboBoxSerializers.Items.AddRange(ArrayOfSerializers);
        }
        private void ComboBoxCreate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            IFactory factory = (IFactory)ComboBoxCreate.SelectedItem;
            BaseObject obj = factory.CreateObject();
            obj.Update(applicationDataContext, true);
        }
        private void ButtonDeleteObject_Click(object sender, EventArgs e)
        {
            BaseObject selectedItem = (BaseObject)this.ComboBoxObjects.SelectedItem;
            selectedItem.ObjectDeleted(applicationDataContext);
            applicationDataContext.CallObjectDeletedEvent(applicationDataContext.Objects, selectedItem);
        }
        private void ButtonUpdateObject_Click(object sender, EventArgs e)
        {
            BaseObject selectedItem = (BaseObject)this.ComboBoxObjects.SelectedItem;
            selectedItem.Update(applicationDataContext, false);
        }

        private void ButtonSaveSerializedObjects_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ISerializerFactory factory = (ISerializerFactory)ComboBoxSerializers.SelectedItem;
                Serializer serializer = factory.CreateSerializer();
                string serializedObjects = serializer.Serialize(applicationDataContext.Objects);
                File.WriteAllText(SaveFileDialog.FileName, serializedObjects);

                StreamWriter CurFile = File.CreateText(SaveFileDialog.FileName);
                applicationDataContext.Objects.ForEach(Object =>
                {
                    CurFile.Write(Object.GetType());
                    PropertyInfo[] CurObjectProperties = Object.GetType().GetProperties();
                    for (int i = 0; i < CurObjectProperties.Length; i++)
                    {
                        CurFile.Write(' ' + CurObjectProperties[i].GetValue(Object).ToString());
                    }
                    CurFile.WriteLine();
                });
                CurFile.Close();
            }
        }

        private void ButtonDownloadDeserializedObjects_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                ISerializerFactory factory = (ISerializerFactory)ComboBoxSerializers.SelectedItem;
                Serializer serializer = factory.CreateSerializer();
                string serializedObjects = File.ReadAllText(OpenFileDialog.FileName);
                applicationDataContext.Objects = serializer.Deserialize(serializedObjects);
                applicationDataContext.ComboBoxObjectsRefresh();
            }
        }

        private void LabelSerializer_Click(object sender, EventArgs e)
        {

        }

        private void ComboBoxSerializers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
