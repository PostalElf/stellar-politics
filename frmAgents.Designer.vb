<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAgents
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAgents))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colClass = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colLocation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.butGo = New System.Windows.Forms.Button()
        Me.lblDetails = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbPlanetNumber = New System.Windows.Forms.ComboBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.cmbStarName = New System.Windows.Forms.ComboBox()
        Me.cmbFilterStarName = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbFilterPlanetNumber = New System.Windows.Forms.ComboBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(520, 275)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.White
        Me.TabPage1.BackgroundImage = Global.Stellar_Politics.My.Resources.Resources.greenBG
        Me.TabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPage1.Controls.Add(Me.cmbFilterPlanetNumber)
        Me.TabPage1.Controls.Add(Me.cmbFilterStarName)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.DataGridView1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(512, 249)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Oubliette"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colClass, Me.colLocation})
        Me.DataGridView1.Location = New System.Drawing.Point(24, 35)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(412, 132)
        Me.DataGridView1.TabIndex = 7
        '
        'colName
        '
        Me.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        Me.colName.ReadOnly = True
        Me.colName.Width = 120
        '
        'colClass
        '
        Me.colClass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colClass.HeaderText = "Specialty"
        Me.colClass.Name = "colClass"
        Me.colClass.ReadOnly = True
        Me.colClass.Width = 80
        '
        'colLocation
        '
        Me.colLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colLocation.HeaderText = "Current Location"
        Me.colLocation.Name = "colLocation"
        Me.colLocation.ReadOnly = True
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.White
        Me.TabPage2.BackgroundImage = Global.Stellar_Politics.My.Resources.Resources.greenBG
        Me.TabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPage2.Controls.Add(Me.Panel1)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.cmbPlanetNumber)
        Me.TabPage2.Controls.Add(Me.lblName)
        Me.TabPage2.Controls.Add(Me.cmbStarName)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(512, 249)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Agent Details"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.butGo)
        Me.Panel1.Controls.Add(Me.lblDetails)
        Me.Panel1.ForeColor = System.Drawing.Color.White
        Me.Panel1.Location = New System.Drawing.Point(317, 17)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(179, 213)
        Me.Panel1.TabIndex = 5
        '
        'butGo
        '
        Me.butGo.Enabled = False
        Me.butGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.butGo.ForeColor = System.Drawing.Color.White
        Me.butGo.Location = New System.Drawing.Point(44, 180)
        Me.butGo.Name = "butGo"
        Me.butGo.Size = New System.Drawing.Size(98, 25)
        Me.butGo.TabIndex = 1
        Me.butGo.Text = "Go"
        Me.butGo.UseVisualStyleBackColor = True
        Me.butGo.Visible = False
        '
        'lblDetails
        '
        Me.lblDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetails.Location = New System.Drawing.Point(9, 10)
        Me.lblDetails.Name = "lblDetails"
        Me.lblDetails.Size = New System.Drawing.Size(161, 161)
        Me.lblDetails.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(37, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Move:"
        '
        'cmbPlanetNumber
        '
        Me.cmbPlanetNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPlanetNumber.FormattingEnabled = True
        Me.cmbPlanetNumber.Location = New System.Drawing.Point(208, 61)
        Me.cmbPlanetNumber.Name = "cmbPlanetNumber"
        Me.cmbPlanetNumber.Size = New System.Drawing.Size(44, 21)
        Me.cmbPlanetNumber.TabIndex = 3
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.Transparent
        Me.lblName.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.White
        Me.lblName.Location = New System.Drawing.Point(24, 21)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(102, 21)
        Me.lblName.TabIndex = 1
        Me.lblName.Text = "Agent Name"
        '
        'cmbStarName
        '
        Me.cmbStarName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStarName.FormattingEnabled = True
        Me.cmbStarName.Location = New System.Drawing.Point(82, 61)
        Me.cmbStarName.Name = "cmbStarName"
        Me.cmbStarName.Size = New System.Drawing.Size(120, 21)
        Me.cmbStarName.TabIndex = 0
        '
        'cmbFilterStarName
        '
        Me.cmbFilterStarName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFilterStarName.FormattingEnabled = True
        Me.cmbFilterStarName.Location = New System.Drawing.Point(76, 197)
        Me.cmbFilterStarName.Name = "cmbFilterStarName"
        Me.cmbFilterStarName.Size = New System.Drawing.Size(146, 21)
        Me.cmbFilterStarName.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(38, 200)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Filter:"
        '
        'cmbFilterPlanetNumber
        '
        Me.cmbFilterPlanetNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFilterPlanetNumber.FormattingEnabled = True
        Me.cmbFilterPlanetNumber.Location = New System.Drawing.Point(228, 197)
        Me.cmbFilterPlanetNumber.Name = "cmbFilterPlanetNumber"
        Me.cmbFilterPlanetNumber.Size = New System.Drawing.Size(43, 21)
        Me.cmbFilterPlanetNumber.TabIndex = 15
        '
        'frmAgents
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(544, 299)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAgents"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Agents"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colClass As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLocation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmbStarName As System.Windows.Forms.ComboBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents cmbPlanetNumber As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblDetails As System.Windows.Forms.Label
    Friend WithEvents butGo As System.Windows.Forms.Button
    Friend WithEvents cmbFilterStarName As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbFilterPlanetNumber As System.Windows.Forms.ComboBox
End Class
