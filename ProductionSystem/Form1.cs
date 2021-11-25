using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductionSystem
{
    public partial class Form1 : Form
    {
        HashSet<string> facts;
        HashSet<Rule> rules;
        public Form1()
        {
            InitializeComponent();
            var factsAndRules = Functions.Parse("../../database.txt");
            facts = factsAndRules.Item1;
            rules = factsAndRules.Item2;
            fillFacts();
            text_box.Height = facts_box.Height;
        }

        private void fillFacts()
        {
            foreach (var facts in facts)
            {
                facts_box.Items.Add(facts);
            }
        }

        public List<string> Difference(List<string> lst1, List<string> lst2)
        {
            List<string> resultSet = new List<string>();
            foreach (var elem in lst1)
            {
                if (!lst2.Contains(elem))
                    resultSet.Add(elem);
            }
            return resultSet;
        }
        private void forward_btn_Click(object sender, EventArgs e)
        {
            var selectedFacts = facts_box.CheckedItems.Cast<string>().ToHashSet();
            var count = selectedFacts.Count + 1;
            int step = 1;
            text_box.Text += "Прямой поиск\n";
            text_box.Text += "Выбранные факты:\n";
            int factNum = 1;
            foreach (var fact in selectedFacts)
            {
                text_box.Text +=$"{factNum}:  {fact}\n";
                factNum++;
            }
            text_box.Text += '\n';
            while (selectedFacts.Count != count)
            {
                count = selectedFacts.Count();
                foreach (var rule in rules)
                {
                    if (!rule.Evaluated && rule.Conditions.All(x => selectedFacts.Contains(x)))
                    {
                        selectedFacts.Add(rule.Action);
                        text_box.Text += $"\nШаг # {step}\n";
                        text_box.Text += "--------------------------------------------------------------\n";
                        text_box.Text += $"Комментарий: {rule.Explanation}\n";
                        text_box.Text += $"Применяемое правило: {rule}\n";
                        text_box.Text += "--------------------------------------------------------------\n";
                        text_box.Text += $"Полученный факт: {rule.Action}\n";
                        rule.Evaluated = true;
                        break;
                    }
                }
                step++;
            }
        }

        private void backword_btn_Click(object sender, EventArgs e)
        {
            /*var selectedFacts = facts_box.CheckedItems.Cast<string>().ToHashSet<string>();
            var res1 = Functions.Backward(selectedFacts, rules);
            var res = selectedFacts;
            var count = res.Count + 1;
            int step = 1;
            text_box.Text += "Обратный поиск\n";
            text_box.Text += "Выбранные факты:\n";
            int factNum = 1;
            foreach (var fact in selectedFacts)
            {
                text_box.Text += $"{factNum}:  {fact}\n";
                factNum++;
            }
            text_box.Text += '\n';*/
           /*foreach (var rule in rules)
            {
                if (!rule.Evaluated && res.Contains(rule.Action))
                {
                    foreach (var condition in rule.Conditions)
                        res.Add(condition);
                    rule.Evaluated = true;
                }
            }*/
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            facts_box.Items.Clear();
            fillFacts();
            text_box.Text = "";
        }
    }
}
