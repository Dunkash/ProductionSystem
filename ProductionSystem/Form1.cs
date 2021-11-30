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
            ReLoadData();
            fillFacts();
            text_box.Height = facts_box.Height;
        }
        private void ReLoadData()
        {
            foreach(var rule in rules)
            {
                rule.Evaluated = false;
            }
        }
        private void fillFacts()
        {
            foreach (var fact in facts)
            {
                facts_box.Items.Add(fact);
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
            ReLoadData();
            text_box.Text = "";
            var selectedFacts = facts_box.CheckedItems.Cast<string>().ToHashSet();
            var dFacts = d_facts_box.CheckedItems.Cast<string>().ToHashSet();
            selectedFacts = selectedFacts.Union(dFacts).ToHashSet();
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
            bool check = true;
            while (check)
            {
                check = false;
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
                        check = true;
                        break;
                    }
                }
                step++;
            }
            var sel = facts_box.CheckedItems.Cast<string>().ToHashSet();
            var res = Difference(selectedFacts.ToList(), sel.ToList());
            text_box.Text += "--------------------------------------------------------------\n";
            text_box.Text += "Полученные факты:\n";
            factNum = 1;
            foreach (var fact in res)
            {
                if (!d_facts_box.Items.Contains(fact))
                {
                    d_facts_box.Items.Add(fact);
                    text_box.Text += $"{factNum}:  {fact}\n";
                    factNum++;
                }
            }
            label3.Visible = true;
            d_facts_box.Visible = true;
            backword_btn.Visible = true;
        }

        private void backword_btn_Click(object sender, EventArgs e)
        {
            ReLoadData();
            text_box.Text = "";
            var selectedFacts = d_facts_box.CheckedItems.Cast<string>().ToHashSet();
            text_box.Text += "Обратный поиск\n";
            text_box.Text += "Выбранные факты:\n";
            int factNum = 1;
            foreach (var fact in selectedFacts)
            {
                text_box.Text += $"{factNum}:  {fact}\n";
                factNum++;
            }
            text_box.Text += '\n';
            var solution = Functions.Prove(selectedFacts.First(), facts, rules);
            text_box.Text += $"\nШаг # {1}\n";
            text_box.Text += "--------------------------------------------------------------\n";
            text_box.Text += $"Полученное правило: {solution.Rule}\n";
            text_box.Text += "--------------------------------------------------------------\n";
            for (int i = 0; i + 1 < solution.Proves.Count; i++)
            {
                text_box.Text += $"\nШаг # {i+2}\n";
                text_box.Text += "--------------------------------------------------------------\n";
                text_box.Text += $"Полученное правило: {solution.Proves[i]}\n";
                text_box.Text += "--------------------------------------------------------------\n";
            }

        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            ReLoadData();
            facts_box.Items.Clear();
            d_facts_box.Items.Clear();
            fillFacts();
            text_box.Text = "";
            d_facts_box.Text = "";
            label3.Visible = false;
            d_facts_box.Visible = false;
            backword_btn.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
