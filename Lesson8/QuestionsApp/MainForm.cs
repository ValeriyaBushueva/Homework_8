using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuestionsApp
{ 
    //Бушуева Валерия
    //Создать приложение, показанное на уроке, добавив в него защиту от возможных ошибок (не создана база данных, обращение к несуществующему вопросу, открытие слишком большого файла и т.д.).//б) Изменить интерфейс программы, увеличив шрифт, поменяв цвет элементов и добавив другие «косметические» улучшения на свое усмотрение.
   // в) Добавить в приложение меню «О программе» с информацией о программе (автор, версия,авторские права и др.).
   //г) Добавить в приложение сообщение с предупреждением при попытке удалить вопрос.
   //д) Добавить пункт меню Save As, в котором можно выбрать имя для сохранения базы данных (элемент SaveFileDialog).
    public partial class MainForm : Form
    {
        private TrueFalse database;

        public MainForm()
        {
            InitializeComponent();
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuItemNew_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                database = new TrueFalse(saveFileDialog.FileName);
                database.Add("Земля круглая?", true);
                database.Add("Радугу можно увидеть и в полночь?", true);
                database.Add("Рог носорога обладает магической силой?", false);
                database.Add("Если камбалу положить на шахматную доску, она тоже станет клетчатой?", true);
                database.Add("На зиму пингвины улетают на север?", false);
                database.Add("Правда ли что, пауки питаются собственной паутиной?", true);
                database.Add("В Австралии практикуется применение одноразовых школьных досок?", false);
                database.Add("В Японии ученики на доске пишут кисточкой с цветными чернилами?", true);
                database.Add("В некоторые виды цветных карандашей добавляется экстракт моркови для большей прочности грифеля?",
                    false);
                database.Add("Дельфины — это маленькие киты?", true);
                database.Add("В некоторых странах жуков-светляков используют в качестве осветительных приборов?", true);
                database.Add("Летучие мыши могут принимать радиосигналы? ", false);
                
                
                database.Save();

                nudNumber.Minimum = 1;
                nudNumber.Maximum = 12;
                nudNumber.Value = 12;
            }
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                database = new TrueFalse(openFileDialog.FileName);
                database.Load();

                nudNumber.Minimum = 1;
                nudNumber.Maximum = database.Count;
                nudNumber.Value = 1;

                if (database.Count > 0)
                {
                    tbQuestion.Text = database[0].Text;
                    cbTrue.Checked = database[0].TrueFalse;
                }
            }
        }
        //обработчик пункта меню Save
        private void menuItemSave_Click(object sender, EventArgs e)
        {
            if (database != null)
            {
                database.Save();
            }
            else
            {
                MessageBox.Show("Вы успешно сохранили базу вопросов", "База вопросов",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
        // Обработчик события изменения значения numericUpDown
        private void nudNumber_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                tbQuestion.Text = database[(int) nudNumber.Value - 1].Text;
                cbTrue.Checked = database[(int) nudNumber.Value - 1].TrueFalse;
            }
            catch(NullReferenceException ex)
            {
                MessageBox.Show($"Подробности: {ex.Message}", "Данный вопрос отсутствует");
            }
        }
        // Обработчик кнопки Сохранить (вопрос)
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                database[(int) nudNumber.Value - 1].Text = tbQuestion.Text;
                database[(int) nudNumber.Value - 1].TrueFalse = cbTrue.Checked;
            }
            catch(NullReferenceException ex)
            {
                MessageBox.Show($"Подробности: {ex.Message}", "Откройте или создайте файл с вопросами");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // обработчик кнопки добавить
            if (database == null)
            {
                MessageBox.Show("Создайте новую базу данных", "Сообщение");
                return;
            }
            database.Add($"Вопрос #{database.Count + 1}", true);
            nudNumber.Maximum = database.Count;
            nudNumber.Value = database.Count;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // обработчик кнопки удалить
            if (nudNumber.Maximum == 1|| database == null)
                return;
            database.Remove((int)nudNumber.Value - 1);
            nudNumber.Maximum--;
            if (nudNumber.Value > 1)
            {
                nudNumber.Value = nudNumber.Value;
            }
            MessageBox.Show("Вопрос успешно удален", "База вопросов",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Автор: GeekBrains\nРедактор: Валерия Бушуева\nВсе права защищены\nВерсия 1.25а", "О программе");
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 =  new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)  
                return;                                               
            // сохраняем текст в файл                                 
            if(database == null)                                      
            {                                                         
                database = new TrueFalse(saveFileDialog1.FileName);   
                database.Add("123", true);                            
                database.Save();                                      
                nudNumber.Minimum = 1;                                
                nudNumber.Maximum = 1;                                
                nudNumber.Value = 1;                                  
                MessageBox.Show("Файл сохранен");                     
            }                                                         
            else                                                      
            {                                                         
                database.FileName = saveFileDialog1.FileName;         
                database.Save();                                      
                MessageBox.Show("Файл сохранен");                     
            }                                                         
        }
    }
}
