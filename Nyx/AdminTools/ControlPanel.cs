//using MySqlX.XDevAPI;
//using Nyx.Server.Client;
//using Nyx.Server.Database;
//using Nyx.Server.Game;
//using Nyx.Server.Network.GamePackets;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Windows.Forms.VisualStyles;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

//namespace Nyx.Server.AdminTools
//{
//    public partial class ControlPanel : Form
//    {
//        public ControlPanel()
//        {
//            InitializeComponent();
//        }
//        private void btnLoadData_Click(object sender, EventArgs e)
//        {
//            //load the information from the database into the datagridview
//            dataGridView1.DataSource = null;
//            try
//            {
//                using (var context = new Database.Context.ServerDbContext())
//                {
//                    // Load data from the Products table
//                    var _Npcs = context.Npcs.ToList();
//                    // Bind data to DataGridView
//                    dataGridView1.DataSource = _Npcs;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }

//        }
//        private void btnUpdate_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (dataGridView1.SelectedRows.Count == 0)
//                {
//                    MessageBox.Show("Please select a NPC to update.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                if (string.IsNullOrWhiteSpace(textBox7.Text) || string.IsNullOrWhiteSpace(textBox8.Text) || string.IsNullOrWhiteSpace(textBox9.Text)
//                    || string.IsNullOrWhiteSpace(textBox10.Text) || string.IsNullOrWhiteSpace(textBox11.Text) || string.IsNullOrWhiteSpace(textBox12.Text)
//                    || string.IsNullOrWhiteSpace(textBox13.Text) || string.IsNullOrWhiteSpace(textBox14.Text) || string.IsNullOrWhiteSpace(textBox15.Text))
//                {
//                    MessageBox.Show("Please enter a valid information.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                var selectedProduct = (Database.Entities.DbNpc)dataGridView1.SelectedRows[0].DataBoundItem; // Use Book for Book entity
//                uint id = selectedProduct.Id;

//                using (var context = new Database.AbstractDbContext())
//                {
//                    var product = context.Npcs.Find(id);
//                    if (product == null)
//                    {
//                        MessageBox.Show("NPC not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }

//                    product.Id = uint.Parse(textBox7.Text);
//                    product.Name = textBox8.Text;
//                    product.Type = uint.Parse(textBox13.Text);
//                    product.Lookface = uint.Parse(textBox9.Text);
//                    product.Mapid = uint.Parse(textBox10.Text);
//                    product.Cellx = uint.Parse(textBox11.Text);
//                    product.Celly = uint.Parse(textBox12.Text);
//                    product.Task0 = uint.Parse(textBox14.Text);
//                    product.Sort = textBox15.Text;
//                    context.SaveChanges();
//                    MessageBox.Show("NPC updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    btnLoadData.PerformClick();
//                    btnSearch.PerformClick();
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void btnRefresh_Click(object sender, EventArgs e)
//        {
//            foreach (var map in Kernel.Maps.Values)
//            {
//                if (map.Npcs != null)
//                    map.Npcs.Clear();
//                map.LoadNpcs();
//            }
//        }

//        private void btnDelete_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (dataGridView1.SelectedRows.Count == 0)
//                {
//                    MessageBox.Show("Please select a NPC to delete.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                var selectedProduct = (Database.Entities.DbNpc)dataGridView1.SelectedRows[0].DataBoundItem; // Use Book for Book entity
//                uint id = selectedProduct.Id;

//                if (MessageBox.Show("Are you sure you want to delete this NPC?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
//                    return;

//                using (var context = new Database.AbstractDbContext())
//                {
//                    var product = context.Npcs.Find(id); // Use context.Books.Find(id) for Book
//                    if (product == null)
//                    {
//                        MessageBox.Show("NPC not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }

//                    context.Npcs.Remove(product); // Use context.Books for Book
//                    context.SaveChanges();
//                    btnLoadData.PerformClick();
//                    btnSearch.PerformClick();
//                    MessageBox.Show("NPC deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error deleting NPC: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void btnSearch_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                using (var context = new Database.Context.ServerDbContext())
//                {
//                    var query = context.Npcs.AsQueryable();

//                    // Filter by NPC ID
//                    if (!string.IsNullOrWhiteSpace(txtNpcID.Text) && uint.TryParse(txtNpcID.Text, out uint id))
//                        query = query.Where(n => n.Id == id);

//                    var filteredNpcs = query.ToList();
//                    dataGridView1.DataSource = filteredNpcs;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error searching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void btnFilter_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                using (var context = new Database.Context.ServerDbContext())
//                {
//                    var query = context.Npcs.AsQueryable();

//                    // Filter by MapID
//                    if (!string.IsNullOrWhiteSpace(txtMapID.Text) && uint.TryParse(txtMapID.Text, out uint mapid))
//                        query = query.Where(n => n.Mapid == mapid);

//                    // Filter by checkboxes (category column must contain these keywords)
//                    if (checkShop.Checked)
//                        query = query.Where(n => n.Sort != null && n.Sort.Contains("Shop"));
//                    if (checkEvents.Checked)
//                        query = query.Where(n => n.Sort != null && n.Sort.Contains("Events"));
//                    if (checkStatic.Checked)
//                        query = query.Where(n => n.Sort != null && n.Sort.Contains("Static"));
//                    if (checkQuest.Checked)
//                        query = query.Where(n => n.Sort != null && n.Sort.Contains("Quest"));

//                    var filteredNpcs = query.ToList();
//                    dataGridView1.DataSource = filteredNpcs;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error searching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void txtNpcID_TextChanged(object sender, EventArgs e)
//        {

//        }

//        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
//        {
//            if (dataGridView1.SelectedRows.Count > 0)
//            {
//                var selectedProduct = (Database.Entities.DbNpc)dataGridView1.SelectedRows[0].DataBoundItem; // Use Book for Book entity
//                textBox7.Text = selectedProduct.Id.ToString(); // Use Title for Book
//                textBox8.Text = selectedProduct.Name;
//                textBox13.Text = selectedProduct.Type.ToString();
//                textBox9.Text = selectedProduct.Lookface.ToString();
//                textBox10.Text = selectedProduct.Mapid.ToString();
//                textBox11.Text = selectedProduct.Cellx.ToString();
//                textBox12.Text = selectedProduct.Celly.ToString();
//                textBox14.Text = selectedProduct.Task0.ToString();
//                textBox15.Text = selectedProduct.Sort;
//            }
//        }

//        private void btnInsert_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (string.IsNullOrWhiteSpace(textBox7.Text) || string.IsNullOrWhiteSpace(textBox8.Text) || string.IsNullOrWhiteSpace(textBox9.Text)
//                    || string.IsNullOrWhiteSpace(textBox10.Text) || string.IsNullOrWhiteSpace(textBox11.Text) || string.IsNullOrWhiteSpace(textBox12.Text)
//                    || string.IsNullOrWhiteSpace(textBox13.Text) || string.IsNullOrWhiteSpace(textBox14.Text) || string.IsNullOrWhiteSpace(textBox15.Text))
//                {
//                    MessageBox.Show("Please enter a valid information.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                using (var context = new Database.AbstractDbContext())
//                {
//                    // Load data from the Products table
//                    var book = new Database.Entities.DbNpc()
//                    {
//                        Id = uint.Parse(textBox7.Text),
//                        Name = textBox8.Text,
//                        Type = uint.Parse(textBox13.Text),
//                        Lookface = uint.Parse(textBox9.Text),
//                        Mapid = uint.Parse(textBox10.Text),
//                        Cellx = uint.Parse(textBox11.Text),
//                        Celly = uint.Parse(textBox12.Text),
//                        Task0 = uint.Parse(textBox14.Text),
//                        Sort = textBox15.Text
//                    };

//                    context.Npcs.Add(book);
//                    context.SaveChanges();
//                    MessageBox.Show("NPC inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    btnLoadData.PerformClick();
//                    btnSearch.PerformClick();
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void btnSpawn_Click(object sender, EventArgs e)
//        {
//            Client.GameClient client = null;
//            client = Client.GameClient.GetClientFromName(comboBox1.Text);
//            if (client == null)
//                return;

//            if (!Kernel.Maps[ushort.Parse(textBox21.Text)].Npcs.ContainsKey(uint.Parse(textBox24.Text)))
//            {
//                Kernel.Titan = true;
//                Kernel.Titan2 = true;
//                Kernel.AlluringWitchHisCrystals = true;
//                NpcSpawn npc = new NpcSpawn();
//                npc.UID = uint.Parse(textBox24.Text);
//                npc.Mesh = ushort.Parse(textBox22.Text);
//                npc.X = ushort.Parse(textBox20.Text);
//                npc.Y = ushort.Parse(textBox19.Text);
//                npc.MapID = ushort.Parse(textBox21.Text);
//                npc.Type = Enums.NpcType.Talker;
//                Kernel.Maps[ushort.Parse(textBox21.Text)].Npcs.Add(uint.Parse(textBox24.Text), npc);
//                npc.SendSpawn(client);
//                client.Send(new Network.GamePackets.Message("Warning! Titan has appeared at Love Canyon!", System.Drawing.Color.WhiteSmoke, Network.GamePackets.Message.System));
//            }
//        }
//        private void button5_Click(object sender, EventArgs e)
//        {
//            comboBox1.Items.Clear();
//            foreach (var user in Kernel.GamePool.Values)
//            {
//                comboBox1.Items.Add(user.Entity.Name);
//            }
//        }
//    }
//}



