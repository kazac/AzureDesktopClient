namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnLoad = new Button();
            panel1 = new Panel();
            btnSave = new Button();
            tabControl1 = new TabControl();
            tabProducts = new TabPage();
            splitContainer1 = new SplitContainer();
            treeProductCategories = new TreeView();
            splitProducts = new SplitContainer();
            dataGridView2 = new DataGridView();
            productCategoryIdDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            ProductId = new DataGridViewTextBoxColumn();
            ListPrice = new DataGridViewTextBoxColumn();
            Color = new DataGridViewTextBoxColumn();
            nameDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            bsP = new BindingSource(components);
            txtName = new TextBox();
            label5 = new Label();
            txtColor = new TextBox();
            label4 = new Label();
            txtListCost = new TextBox();
            label3 = new Label();
            txtProductId = new TextBox();
            label2 = new Label();
            txtProductCategoryId = new TextBox();
            label1 = new Label();
            bsPC = new BindingSource(components);
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitProducts).BeginInit();
            splitProducts.Panel1.SuspendLayout();
            splitProducts.Panel2.SuspendLayout();
            splitProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsP).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsPC).BeginInit();
            SuspendLayout();
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(17, 8);
            btnLoad.Margin = new Padding(3, 2, 3, 2);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(82, 22);
            btnLoad.TabIndex = 0;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(btnLoad);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1113, 37);
            panel1.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(121, 8);
            btnSave.Margin = new Padding(3, 2, 3, 2);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(82, 22);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabProducts);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 37);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1113, 588);
            tabControl1.TabIndex = 2;
            // 
            // tabProducts
            // 
            tabProducts.Controls.Add(splitContainer1);
            tabProducts.Location = new Point(4, 24);
            tabProducts.Name = "tabProducts";
            tabProducts.Padding = new Padding(3);
            tabProducts.Size = new Size(1105, 560);
            tabProducts.TabIndex = 1;
            tabProducts.Text = "Products";
            tabProducts.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(treeProductCategories);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitProducts);
            splitContainer1.Size = new Size(1099, 554);
            splitContainer1.SplitterDistance = 366;
            splitContainer1.TabIndex = 1;
            // 
            // treeProductCategories
            // 
            treeProductCategories.Dock = DockStyle.Fill;
            treeProductCategories.Location = new Point(0, 0);
            treeProductCategories.Name = "treeProductCategories";
            treeProductCategories.Size = new Size(366, 554);
            treeProductCategories.TabIndex = 0;
            treeProductCategories.AfterSelect += treeProductCategories_AfterSelect;
            // 
            // splitProducts
            // 
            splitProducts.Dock = DockStyle.Fill;
            splitProducts.Location = new Point(0, 0);
            splitProducts.Name = "splitProducts";
            splitProducts.Orientation = Orientation.Horizontal;
            // 
            // splitProducts.Panel1
            // 
            splitProducts.Panel1.Controls.Add(dataGridView2);
            // 
            // splitProducts.Panel2
            // 
            splitProducts.Panel2.Controls.Add(txtName);
            splitProducts.Panel2.Controls.Add(label5);
            splitProducts.Panel2.Controls.Add(txtColor);
            splitProducts.Panel2.Controls.Add(label4);
            splitProducts.Panel2.Controls.Add(txtListCost);
            splitProducts.Panel2.Controls.Add(label3);
            splitProducts.Panel2.Controls.Add(txtProductId);
            splitProducts.Panel2.Controls.Add(label2);
            splitProducts.Panel2.Controls.Add(txtProductCategoryId);
            splitProducts.Panel2.Controls.Add(label1);
            splitProducts.Size = new Size(729, 554);
            splitProducts.SplitterDistance = 306;
            splitProducts.TabIndex = 0;
            // 
            // dataGridView2
            // 
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { productCategoryIdDataGridViewTextBoxColumn1, ProductId, ListPrice, Color, nameDataGridViewTextBoxColumn1 });
            dataGridView2.DataSource = bsP;
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.Location = new Point(0, 0);
            dataGridView2.MultiSelect = false;
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.Size = new Size(729, 306);
            dataGridView2.TabIndex = 0;
            // 
            // productCategoryIdDataGridViewTextBoxColumn1
            // 
            productCategoryIdDataGridViewTextBoxColumn1.DataPropertyName = "ProductCategoryId";
            productCategoryIdDataGridViewTextBoxColumn1.HeaderText = "ProductCategoryId";
            productCategoryIdDataGridViewTextBoxColumn1.Name = "productCategoryIdDataGridViewTextBoxColumn1";
            // 
            // ProductId
            // 
            ProductId.DataPropertyName = "ProductId";
            ProductId.HeaderText = "Id";
            ProductId.Name = "ProductId";
            // 
            // ListPrice
            // 
            ListPrice.DataPropertyName = "ListPrice";
            ListPrice.HeaderText = "List Price";
            ListPrice.Name = "ListPrice";
            // 
            // Color
            // 
            Color.DataPropertyName = "Color";
            Color.HeaderText = "Color";
            Color.Name = "Color";
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            nameDataGridViewTextBoxColumn1.HeaderText = "Name";
            nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            nameDataGridViewTextBoxColumn1.Width = 500;
            // 
            // bsP
            // 
            bsP.DataMember = "Products";
            bsP.DataSource = typeof(VM);
            // 
            // txtName
            // 
            txtName.Location = new Point(217, 117);
            txtName.Name = "txtName";
            txtName.Size = new Size(415, 23);
            txtName.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(79, 120);
            label5.Name = "label5";
            label5.Size = new Size(42, 15);
            label5.TabIndex = 8;
            label5.Text = "Name:";
            // 
            // txtColor
            // 
            txtColor.Location = new Point(532, 76);
            txtColor.Name = "txtColor";
            txtColor.Size = new Size(100, 23);
            txtColor.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(436, 79);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 6;
            label4.Text = "Color:";
            // 
            // txtListCost
            // 
            txtListCost.Location = new Point(217, 76);
            txtListCost.Name = "txtListCost";
            txtListCost.Size = new Size(100, 23);
            txtListCost.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(79, 79);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 4;
            label3.Text = "List Price:";
            // 
            // txtProductId
            // 
            txtProductId.Location = new Point(532, 36);
            txtProductId.Name = "txtProductId";
            txtProductId.ReadOnly = true;
            txtProductId.Size = new Size(100, 23);
            txtProductId.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(436, 39);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 2;
            label2.Text = "Product ID:";
            // 
            // txtProductCategoryId
            // 
            txtProductCategoryId.Location = new Point(217, 36);
            txtProductCategoryId.Name = "txtProductCategoryId";
            txtProductCategoryId.ReadOnly = true;
            txtProductCategoryId.Size = new Size(100, 23);
            txtProductCategoryId.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(79, 39);
            label1.Name = "label1";
            label1.Size = new Size(117, 15);
            label1.TabIndex = 0;
            label1.Text = "Product Category ID:";
            // 
            // bsPC
            // 
            bsPC.DataMember = "ProductCategories";
            bsPC.DataSource = typeof(VM);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1113, 625);
            Controls.Add(tabControl1);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            DataContextChanged += Form1_DataContextChanged;
            panel1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabProducts.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitProducts.Panel1.ResumeLayout(false);
            splitProducts.Panel2.ResumeLayout(false);
            splitProducts.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitProducts).EndInit();
            splitProducts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsP).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsPC).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnLoad;
        private Panel panel1;
        private TabControl tabControl1;
        private TabPage tabProducts;
        private BindingSource bsPC;
        private DataGridViewTextBoxColumn nodifiedDateDataGridViewTextBoxColumn;
        private SplitContainer splitProducts;
        private BindingSource bsP;
        private DataGridView dataGridView2;
        private TextBox txtProductId;
        private Label label2;
        private TextBox txtProductCategoryId;
        private Label label1;
        private TextBox txtName;
        private Label label5;
        private TextBox txtColor;
        private Label label4;
        private TextBox txtListCost;
        private Label label3;
        private Button btnSave;
        private SplitContainer splitContainer1;
        internal TreeView treeProductCategories;
        private DataGridViewTextBoxColumn productCategoryIdDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn ProductId;
        private DataGridViewTextBoxColumn ListPrice;
        private DataGridViewTextBoxColumn Color;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
    }
}