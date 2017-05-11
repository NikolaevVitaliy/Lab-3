using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;


 //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace View
{
    public partial class CreatingForm : Form
    {
        private bool _maskedFirstName = false;
        private bool _maskedLastName = false;
        private bool _maskedDateOfReceipt = false;
        private bool _maskedWorkingDays = false;
        private bool _maskedWeekend = false;
        private bool _maskedTimeOff = false;
        private bool _maskedBaseSalary = false;
        private bool _maskedHour = false;
        private bool _maskedNumberChange = false;
        private bool _maskedMoneyHour = false;
        private bool _maskedMoneyOneChange = false;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




        public CreatingForm()
        {
            InitializeComponent();
            #if DEBUG
            buttonCreateRandomData.Visible = true;
            #endif
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void radioButtonSalaryRate_CheckedChanged(object sender, EventArgs e)
        {
            maskedTextBoxWorkingDays.Visible = false;
            maskedTextBoxWeekend.Visible = false;
            maskedTextBoxTimeOff.Visible = false;
            maskedTextBoxBaseSalary.Visible = false;
            maskedTextBoxHour.Visible = false;
            maskedTextBoxNumberChange.Visible = true;
            maskedTextBoxMoneyHour.Visible = false;
            maskedTextBoxMoneyOneChange.Visible = true;

        }

        private void radioButtonSalary_CheckedChanged(object sender, EventArgs e)
        {
            maskedTextBoxWorkingDays.Visible = true;
            maskedTextBoxWeekend.Visible = true;
            maskedTextBoxTimeOff.Visible = true;
            maskedTextBoxBaseSalary.Visible = true;
            maskedTextBoxHour.Visible = false;
            maskedTextBoxNumberChange.Visible = false;
            maskedTextBoxMoneyHour.Visible = false;
            maskedTextBoxMoneyOneChange.Visible = false;
        }

        private void radioButtonSalaryHourly_CheckedChanged(object sender, EventArgs e)
        {
            maskedTextBoxWorkingDays.Visible = false;
            maskedTextBoxWeekend.Visible = false;
            maskedTextBoxTimeOff.Visible = false;
            maskedTextBoxBaseSalary.Visible = false;
            maskedTextBoxHour.Visible = true;
            maskedTextBoxNumberChange.Visible = false;
            maskedTextBoxMoneyHour.Visible = true;
            maskedTextBoxMoneyOneChange.Visible = false;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Кнопка отмены
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Кнопка добавления данных о человеке в список из формы создания 
        private void buttonОК_Click(object sender, EventArgs e)
        {
            bool a = false;
            if ((textBoxFirstName.Text != "") || (textBoxLastName.Text != "") || (maskedTextBoxDateOfReceipt.Text != "") ||
                (maskedTextBoxWorkingDays.Text != "") || (maskedTextBoxWeekend.Text != "") || (maskedTextBoxTimeOff.Text != "") ||
                (maskedTextBoxBaseSalary.Text != "") || (maskedTextBoxHour.Text != "") || (maskedTextBoxNumberChange.Text != "") || (maskedTextBoxMoneyHour.Text != "")
                || (maskedTextBoxMoneyOneChange.Text != ""))
            {
                a = true;
            }
            if (!(_maskedFirstName && _maskedLastName && _maskedDateOfReceipt) && !(a))
            {
                MessageBox.Show("Введите фамилию имя и дату приема на работу!");
            }
            else if ((radioButtonSalaryRate.Checked) && !(a))
            {
                if (!(_maskedNumberChange && _maskedMoneyOneChange))
                    MessageBox.Show("Введите кол-во смен и кол-во денег за одну смену!");
                else
                    a = true;
            }

            else if ((radioButtonSalary.Checked) && !(a))
            {
                if (!(_maskedBaseSalary && _maskedWorkingDays && _maskedWeekend && _maskedTimeOff))
                    MessageBox.Show("Введите базовую зарплату, рабочие дни, выходные, и отгулы!");
                else
                    a = true;
            }

            else if ((radioButtonSalaryHourly.Checked) && !(a))
            {
                if (!(_maskedHour && _maskedMoneyHour))
                    MessageBox.Show("Введите кол-во часов и кол-во денег за один час!");
                else
                    a = true;
            }


            if (a)
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private ISalary _person;


        //Описание данных о человеке
        public ISalary Person
        {
            get
            {
                if (radioButtonSalaryRate.Checked)
                {
                    var Rate = new Model.SalaryRate();
                    Rate.FirstName = Convert.ToString(textBoxFirstName.Text);
                    Rate.LastName = Convert.ToString(textBoxLastName.Text);
                    Rate.DateOfReceipt = Convert.ToInt32(maskedTextBoxDateOfReceipt.Text);

                    Rate.NumberChange = Convert.ToInt32(maskedTextBoxNumberChange.Text);
                    Rate.MoneyOneChange = Convert.ToDouble(maskedTextBoxMoneyOneChange.Text);
                    _person = Rate;
                }


                else if (radioButtonSalary.Checked)
                {
                    var Salary = new Model.Salary();
                    Salary.FirstName = Convert.ToString(textBoxFirstName.Text);
                    Salary.LastName = Convert.ToString(textBoxLastName.Text);
                    Salary.DateOfReceipt = Convert.ToInt32(maskedTextBoxDateOfReceipt.Text);

                    Salary.Basesalary = Convert.ToDouble(maskedTextBoxBaseSalary.Text);
                    Salary.WorkingDays = Convert.ToInt32(maskedTextBoxWorkingDays.Text);
                    Salary.Weekend = Convert.ToInt32(maskedTextBoxWeekend.Text);
                    Salary.Timeoff = Convert.ToInt32(maskedTextBoxTimeOff.Text);

                    _person = Salary;
                }

                else if (radioButtonSalaryHourly.Checked)
                {
                    var Hourly = new Model.SalaryHourly();
                    Hourly.FirstName = Convert.ToString(textBoxFirstName.Text);
                    Hourly.LastName = Convert.ToString(textBoxLastName.Text);
                    Hourly.DateOfReceipt = Convert.ToInt32(maskedTextBoxDateOfReceipt.Text);

                    Hourly.Hour = Convert.ToInt32(maskedTextBoxHour.Text);
                    Hourly.Money = Convert.ToDouble(maskedTextBoxMoneyHour.Text);

                    _person = Hourly;
                }
                return _person;
            }

            set
            {
                _person = value;
                textBoxFirstName.Text = Convert.ToString(value.FirstName);
                textBoxLastName.Text = Convert.ToString(value.LastName);
                maskedTextBoxDateOfReceipt.Text = Convert.ToString(value.DateOfReceipt);

                if (value is Model.SalaryRate)
                {
                    var Rate = (Model.SalaryRate)value;
                    maskedTextBoxNumberChange.Text = Convert.ToString(Rate.NumberChange);
                    maskedTextBoxMoneyOneChange.Text = Convert.ToString(Rate.MoneyOneChange);
                }
                else if (value is Model.Salary)
                {
                    var Salary = (Model.Salary)value;
                    maskedTextBoxBaseSalary.Text = Convert.ToString(Salary.Basesalary);
                    maskedTextBoxWorkingDays.Text = Convert.ToString(Salary.WorkingDays);
                    maskedTextBoxWeekend.Text = Convert.ToString(Salary.Weekend);
                    maskedTextBoxTimeOff.Text = Convert.ToString(Salary.Timeoff);
                }

                else if (value is Model.SalaryHourly)
                {
                    var Hourly = (Model.SalaryHourly)value;
                    maskedTextBoxHour.Text = Convert.ToString(Hourly.Hour);
                    maskedTextBoxMoneyHour.Text = Convert.ToString(Hourly.Money);

                }
            }
        }




        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        //Кнопка  расчета зарплаты
        private void buttonCalculateSalary_Click(object sender, EventArgs e)
        {
            if ((_maskedNumberChange && _maskedMoneyOneChange) || (_maskedWorkingDays && _maskedWeekend && _maskedTimeOff && _maskedBaseSalary) || (_maskedHour && _maskedMoneyHour))
            { textBox3.Text = Convert.ToString(Person.GetSalary()); }
            else
                MessageBox.Show("Не все величины были введены");
        }
        private void textBoxFirstName_Leave(object sender, EventArgs e)
        {
            if (textBoxFirstName.Text == "     ,")
            {
                errorProviderFirstname.SetError(textBoxFirstName, "Данное поле должно быть заполненным");
            }
            else
            {
                errorProviderFirstname.Clear();
                _maskedFirstName = true;
            }
        }

        private void textBoxLastName_Leave(object sender, EventArgs e)
        {
            if (textBoxLastName.Text == ",")
            {
                errorProviderLastname.SetError(textBoxLastName, "Данное поле должно быть заполненным");
            }
            else
            {
                errorProviderLastname.Clear();
                _maskedLastName = true;
            }
        }

        private void maskedTextBoxDateOfReceipt_Leave(object sender, EventArgs e)
        {
            if (maskedTextBoxDateOfReceipt.Text == "     ,")
            {
                errorProviderDateOfReceipt.SetError(maskedTextBoxDateOfReceipt, "Данное поле должно быть заполненным");
            }
            else
            {
                errorProviderDateOfReceipt.Clear();
                _maskedDateOfReceipt = true;
            }
        }
        private void maskedTextBoxWorkingDays_Leave(object sender, EventArgs e)
        {
            if (maskedTextBoxWorkingDays.Text == "     ,")
            {
                errorProviderWorkingDays.SetError(maskedTextBoxWorkingDays, "Данное поле должно быть заполненным");
            }
            else
            {
                errorProviderWorkingDays.Clear();
                _maskedWorkingDays = true;
            }
        }

        private void maskedTextBoxWeekend_Leave(object sender, EventArgs e)
        {
            if (maskedTextBoxWeekend.Text == "     ,")
            {
                errorProviderWeekend.SetError(maskedTextBoxWeekend, "Данное поле должно быть заполненным");
            }
            else
            {
                errorProviderWeekend.Clear();
                _maskedWeekend = true;
            }
        }

        private void maskedTextBoxTimeOff_Leave(object sender, EventArgs e)
        {
            if (maskedTextBoxTimeOff.Text == "     ,")
            {
                errorProviderTimeOff.SetError(maskedTextBoxTimeOff, "Данное поле должно быть заполненным");
            }
            else
            {
                errorProviderTimeOff.Clear();
                _maskedTimeOff = true;
            }
        }

        private void maskedTextBoxBaseSalary_Leave(object sender, EventArgs e)
        {
            if (maskedTextBoxBaseSalary.Text == "     ,")
            {
                errorProviderBaseSalary.SetError(maskedTextBoxBaseSalary, "Данное поле должно быть заполненным");
            }
            else
            {
                errorProviderBaseSalary.Clear();
                _maskedBaseSalary = true;
            }
        }

        private void maskedTextBoxHour_Leave(object sender, EventArgs e)
        {
            if (maskedTextBoxHour.Text == "     ,")
            {
                errorProviderHour.SetError(maskedTextBoxHour, "Данное поле должно быть заполненным");
            }
            else
            {
                errorProviderHour.Clear();
                _maskedHour = true;
            }
        }

        private void maskedTextBoxNumberChange_Leave(object sender, EventArgs e)
        {
            if (maskedTextBoxNumberChange.Text == "     ,")
            {
                errorProviderNumberChange.SetError(maskedTextBoxNumberChange, "Данное поле должно быть заполненным");
            }
            else
            {
                errorProviderNumberChange.Clear();
                _maskedNumberChange = true;
            }
        }

        private void maskedTextBoxMoneyHour_Leave(object sender, EventArgs e)
        {
            if (maskedTextBoxMoneyHour.Text == "     ,")
            {
                errorProviderMoneyHour.SetError(maskedTextBoxMoneyHour, "Данное поле должно быть заполненным");
            }
            else
            {
                errorProviderMoneyHour.Clear();
                _maskedMoneyHour = true;
            }
        }

        private void maskedTextBoxMoneyOneChange_Leave(object sender, EventArgs e)
        {
            if (maskedTextBoxMoneyOneChange.Text == "     ,")
            {
                errorProviderMoneyOneChange.SetError(maskedTextBoxMoneyOneChange, "Данное поле должно быть заполненным");
            }
            else
            {
                errorProviderMoneyOneChange.Clear();
                _maskedMoneyOneChange = true;
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////


        //Кнопка  заполнения случайными величинами поле
        private void buttonCreateRandomData_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int personsalary = random.Next();
            textBoxFirstName.Text = Convert.ToString(random.ToString());
            textBoxLastName.Text = Convert.ToString(random.ToString());
            maskedTextBoxDateOfReceipt.Text = Convert.ToString(1.111 + random.NextDouble() * (999.999 - 1.111));
            _maskedFirstName = true;
            _maskedLastName = true;
            _maskedDateOfReceipt = true;

            if (radioButtonSalaryRate.Checked)
            {

                maskedTextBoxNumberChange.Text = Convert.ToString(10 + random.Next(31));
                maskedTextBoxMoneyOneChange.Text = Convert.ToString(50 + random.NextDouble() * (3000.99 - 1.11));
                _maskedNumberChange = true;
                _maskedMoneyOneChange = true;
            }

            else if (radioButtonSalary.Checked)
            {
                maskedTextBoxBaseSalary.Text = Convert.ToString(50 + random.NextDouble() * (3000.99-1.11));
                maskedTextBoxWorkingDays.Text = Convert.ToString(1.111 + random.NextDouble() * (99.999 - 1.111));
                maskedTextBoxWeekend.Text = Convert.ToString(1.111 + random.NextDouble() * (99.999 - 1.111));
                maskedTextBoxTimeOff.Text = Convert.ToString(1.111 + random.NextDouble() * (99.999 - 1.111));
                _maskedBaseSalary = true;
                _maskedWorkingDays = true;
                _maskedWeekend = true;
                _maskedTimeOff = true;
            }
            else if (radioButtonSalaryHourly.Checked)
            {
                maskedTextBoxHour.Text = Convert.ToString(1 + random.Next(200));
                maskedTextBoxMoneyHour.Text = Convert.ToString(50 + random.NextDouble()*(1000.99-1.11));
                _maskedHour = true;
                _maskedMoneyHour = true;
            }
        }
    }
}

























        

       




        
   
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    

