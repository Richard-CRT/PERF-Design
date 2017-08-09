using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PERF_Design
{
    public partial class MainForm : Form
    {
        public const string Title = "PERF DESIGN";
        public GridContainer GridContainer;
        public string FileName;
        public bool Saved;

        public MainForm()
        {
            InitializeComponent();
            Saved = true;
            FileName = null;
            UpdateOpenFile();

            //TrackingConnectionHole = null;

            Preferences.GridHeight = 50;
            Preferences.GridWidth = 50;

            PanelGridContainer.Width = (int)(Preferences.GridWidth * Preferences.GridSize);
            PanelGridContainer.Height = (int)(Preferences.GridHeight * Preferences.GridSize);

            GridContainer = new GridContainer(this);
            GridContainer.Width = PanelGridContainer.Width;
            GridContainer.Height = PanelGridContainer.Height;
            PanelGridContainer.Controls.Add(GridContainer);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckBoxSolderTool.Checked = true;
        }

        private void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            MainForm_Scroll(null, null);
        }

        private void MainForm_Scroll(object sender, ScrollEventArgs e)
        {
            PanelControlPanel.Location = new Point(-1, -1);
        }

        private void ButtonCancelPlacing_Click(object sender, EventArgs e)
        {
            GridContainer.ResetSuggestions();
        }

        private void LoadNewBoard(Save saveObj = null)
        {
            PanelGridContainer.Width = (int)(Preferences.GridWidth * Preferences.GridSize);
            PanelGridContainer.Height = (int)(Preferences.GridHeight * Preferences.GridSize);
            
            UpdateOpenFile();
            PanelGridContainer.Controls.Clear();
            GridContainer = new GridContainer(this,saveObj);
            GridContainer.Width = PanelGridContainer.Width;
            GridContainer.Height = PanelGridContainer.Height;
            PanelGridContainer.Controls.Add(GridContainer);
        }

        private void ButtonNewBoard_Click(object sender, EventArgs e)
        {
            FileName = null;
            LoadNewBoard();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (FileName != null)
            {
                Save(FileName);
            }
            else
            {
                ButtonSaveAs_Click(null, null);
            }
        }

        private void ButtonSaveAs_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Save(SaveFileDialog.FileName);
            }
        }

        private void UpdateOpenFile()
        {
            if (FileName != null)
            {
                this.Text = MainForm.Title + " - " + FileName;
            }
            else
            {
                this.Text = MainForm.Title;
            }
            if (!Saved)
            {
                this.Text = this.Text + " *";
            }
        }

        private void Save(string filename)
        {
            Save newSave = new Save();
            newSave.Width = Preferences.GridWidth;
            newSave.Height = Preferences.GridHeight;
            newSave.SolderConnections = GridContainer.SolderConnections;
            newSave.WireConnections = GridContainer.WireConnections;
            newSave.Chips = GridContainer.Chips;
            SaveFileDialog.FileName = "";

            // To serialize the hashtable and its key/value pairs,  
            // you must first open a stream for writing. 
            // In this case, use a file stream.
            FileStream fs = new FileStream(filename, FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, newSave);
                Saved = true;
                FileName = filename;
                UpdateOpenFile();
            }
            catch (SerializationException err)
            {
                Console.WriteLine("Failed to serialize. Reason: " + err.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog.FileName = "";
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Open the file containing the data that you want to deserialize.
                Save oldSave = LoadSave(OpenFileDialog.FileName);

                Preferences.GridWidth = oldSave.Width;
                Preferences.GridHeight = oldSave.Height;
                
                LoadNewBoard(oldSave);
            }
        }

        private Save LoadSave(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            Save oldSave = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and 
                FileName = OpenFileDialog.FileName;
                // assign the reference to the local variable.
                oldSave = (Save)formatter.Deserialize(fs);
                FileName = filename;
                Saved = true;
            }
            catch (SerializationException err)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + err.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
            return oldSave;
        }

        private void CheckBoxSolderTool_Click(object sender, EventArgs e)
        {
            GridContainer.SelectedTool = Tool.Solder;
            CheckBoxSolderTool.Checked = true;
            CheckBoxEraseSolderTool.Checked = false;
            CheckBoxWireTool.Checked = false;
            CheckBoxEraseWireTool.Checked = false;
            CheckBoxChipTool.Checked = false;
            CheckBoxRotateChipTool.Checked = false;
            CheckBoxEraseChipTool.Checked = false;
            GridContainer.ResetSuggestions();
        }

        private void CheckBoxEraseSolderTool_Click(object sender, EventArgs e)
        {
            GridContainer.SelectedTool = Tool.EraseSolder;
            CheckBoxSolderTool.Checked = false;
            CheckBoxEraseSolderTool.Checked = true;
            CheckBoxWireTool.Checked = false;
            CheckBoxEraseWireTool.Checked = false;
            CheckBoxChipTool.Checked = false;
            CheckBoxRotateChipTool.Checked = false;
            CheckBoxEraseChipTool.Checked = false;
            GridContainer.ResetSuggestions();
        }

        private void CheckBoxWireTool_Click(object sender, EventArgs e)
        {
            GridContainer.SelectedTool = Tool.Wire;
            CheckBoxSolderTool.Checked = false;
            CheckBoxEraseSolderTool.Checked = false;
            CheckBoxWireTool.Checked = true;
            CheckBoxEraseWireTool.Checked = false;
            CheckBoxChipTool.Checked = false;
            CheckBoxRotateChipTool.Checked = false;
            CheckBoxEraseChipTool.Checked = false;
            GridContainer.ResetSuggestions();
        }

        private void CheckBoxEraseWireTool_Click(object sender, EventArgs e)
        {
            GridContainer.SelectedTool = Tool.EraseWire;
            CheckBoxSolderTool.Checked = false;
            CheckBoxEraseSolderTool.Checked = false;
            CheckBoxWireTool.Checked = false;
            CheckBoxEraseWireTool.Checked = true;
            CheckBoxChipTool.Checked = false;
            CheckBoxRotateChipTool.Checked = false;
            CheckBoxEraseChipTool.Checked = false;
            GridContainer.ResetSuggestions();
        }

        private void CheckBoxChipTool_Click(object sender, EventArgs e)
        {
            GridContainer.SelectedTool = Tool.Chip;
            CheckBoxSolderTool.Checked = false;
            CheckBoxEraseSolderTool.Checked = false;
            CheckBoxWireTool.Checked = false;
            CheckBoxEraseWireTool.Checked = false;
            CheckBoxChipTool.Checked = true;
            CheckBoxRotateChipTool.Checked = false;
            CheckBoxEraseChipTool.Checked = false;
            GridContainer.ResetSuggestions();
        }

        private void CheckBoxRotateChipTool_Click(object sender, EventArgs e)
        {
            GridContainer.SelectedTool = Tool.RotateChip;
            CheckBoxSolderTool.Checked = false;
            CheckBoxEraseSolderTool.Checked = false;
            CheckBoxWireTool.Checked = false;
            CheckBoxEraseWireTool.Checked = false;
            CheckBoxChipTool.Checked = false;
            CheckBoxRotateChipTool.Checked = true;
            CheckBoxEraseChipTool.Checked = false;
            GridContainer.ResetSuggestions();
        }

        private void CheckBoxEraseChipTool_Click(object sender, EventArgs e)
        {
            GridContainer.SelectedTool = Tool.EraseChip;
            CheckBoxSolderTool.Checked = false;
            CheckBoxEraseSolderTool.Checked = false;
            CheckBoxWireTool.Checked = false;
            CheckBoxEraseWireTool.Checked = false;
            CheckBoxChipTool.Checked = false;
            CheckBoxRotateChipTool.Checked = false;
            CheckBoxEraseChipTool.Checked = true;
            GridContainer.ResetSuggestions();
        }

        public void PlacingStarted()
        {
            ButtonCancelPlacing.Enabled = true;
        }

        public void PlacingFinished()
        {
            ButtonCancelPlacing.Enabled = false;
        }

        public void ChangeMade()
        {
            Saved = false;
            UpdateOpenFile();
        }

        protected override System.Drawing.Point ScrollToControl(Control activeControl)
        {
            // When there's only 1 control in the panel and the user clicks
            //  on it, .NET tries to scroll to the control. This invariably
            //  forces the panel to scroll up. This little hack prevents that.
            return DisplayRectangle.Location;
        }
    }

    [Serializable]
    public class Save
    {
        public int Width;
        public int Height;
        public List<Connection> SolderConnections;
        public List<Connection> WireConnections;
        public List<Chip> Chips;
    }

    public class GridContainer : UserControl
    {
        public Hole[][] Grid;
        public Tool SelectedTool;
        private MainForm FormParent;

        public Hole HoveredHole;

        public List<Connection> SolderConnections;
        public Connection SuggestedSolderConnection;
        public List<Connection> WireConnections;
        public Connection SuggestedWireConnection;
        public List<Chip> Chips;
        public Chip SuggestedChip;
        public bool MousePressed;

        public GridContainer(MainForm parentForm, Save saveFile = null)
        {
            this.DoubleBuffered = true;
            FormParent = parentForm;

            this.MouseMove += GridContainer_MouseMove;
            this.MouseDown += GridContainer_MouseDown;
            this.MouseUp += GridContainer_MouseUp;
            this.MouseLeave += GridContainer_MouseLeave;

            SolderConnections = new List<Connection>();
            WireConnections = new List<Connection>();
            Chips = new List<Chip>();
            if (saveFile != null)
            {
                if (saveFile.SolderConnections != null)
                {
                    SolderConnections = saveFile.SolderConnections;
                }

                if (saveFile.WireConnections != null)
                {
                    WireConnections = saveFile.WireConnections;
                }

                if (saveFile.Chips != null)
                {
                    Chips = saveFile.Chips;
                }
            }

            SuggestedSolderConnection = new Connection();
            SuggestedWireConnection = new Connection();
            SuggestedChip = new Chip();
            this.Cursor = Cursors.Hand;
            HoveredHole = null;

            Grid = new Hole[Preferences.GridHeight][];
            for (int y = 0; y < Preferences.GridHeight; y++)
            {
                Grid[y] = new Hole[Preferences.GridWidth];
                for (int x = 0; x < Preferences.GridWidth; x++)
                {
                    Hole newHole = new Hole(this, x, y);
                    Grid[y][x] = newHole;
                    //this.Controls.Add(newHole);
                    newHole.OnMouseClickExt += HoleClicked;
                    newHole.OnHoleHover += HoleHovered;
                }
            }
        }

        private void GridContainer_MouseLeave(object sender, EventArgs e)
        {
            HoveredHole.OnMouseLeave();
            HoveredHole = null;
        }

        private void GridContainer_MouseDown(object sender, MouseEventArgs e)
        {
            MousePressed = true;
            HoveredHole.Redraw();
            if (HoveredHole != null)
            {
                HoveredHole.OnMouseClick(e);
            }
        }

        private void GridContainer_MouseUp(object sender, MouseEventArgs e)
        {
            MousePressed = false;
            HoveredHole.Redraw();
        }

        private void GridContainer_MouseMove(object sender, MouseEventArgs e)
        {
            int x = (int)Math.Floor(e.Location.X / (double)Preferences.GridSize);
            int y = (int)Math.Floor(e.Location.Y / (double)Preferences.GridSize);
            // hover
            Hole hole = Grid[y][x];
            if (HoveredHole != hole)
            {
                // different hole hovered now
                if (HoveredHole != null)
                {
                    HoveredHole.OnMouseLeave();
                }
                hole.OnMouseEnter();
                HoveredHole = hole;
            }
        }

        public void ResetSuggestions()
        {
            Hole tempHole1 = null;
            if (SuggestedSolderConnection.Hole1Y != -1)
            {
                tempHole1 = Grid[SuggestedSolderConnection.Hole1Y][SuggestedSolderConnection.Hole1X];
            }
            Hole tempHole2 = null;
            if (SuggestedSolderConnection.Hole2Y != -1)
            {
                tempHole2 = Grid[SuggestedSolderConnection.Hole2Y][SuggestedSolderConnection.Hole2X];
            }
            SuggestedSolderConnection = new Connection();
            if (tempHole1 != null)
            {
                tempHole1.Redraw();
            }
            if (tempHole2 != null)
            {
                tempHole2.Redraw();
            }

            tempHole1 = null;
            if (SuggestedWireConnection.Hole1Y != -1)
            {
                tempHole1 = Grid[SuggestedWireConnection.Hole1Y][SuggestedWireConnection.Hole1X];
            }
            tempHole2 = null;
            if (SuggestedWireConnection.Hole2Y != -1)
            {
                tempHole2 = Grid[SuggestedWireConnection.Hole2Y][SuggestedWireConnection.Hole2X];
            }
            SuggestedWireConnection = new Connection();
            if (tempHole1 != null)
            {
                tempHole1.Redraw();
            }
            if (tempHole2 != null)
            {
                tempHole2.Redraw();
            }

            tempHole1 = null;
            if (SuggestedChip.TopLeftHoleY != -1)
            {
                tempHole1 = Grid[SuggestedChip.TopLeftHoleY][SuggestedChip.TopLeftHoleX];
            }
            tempHole2 = null;
            if (SuggestedChip.BottomRightHoleY != -1)
            {
                tempHole2 = Grid[SuggestedChip.BottomRightHoleY][SuggestedChip.BottomRightHoleX];
            }
            SuggestedChip = new Chip();
            if (tempHole1 != null)
            {
                tempHole1.Redraw();
            }
            if (tempHole2 != null)
            {
                tempHole2.Redraw();
            }

            FormParent.PlacingFinished();
        }

        public void HoleHovered(Hole hole)
        {
            if ((SelectedTool == Tool.Solder || SelectedTool == Tool.EraseSolder) && SuggestedSolderConnection.Hole1Y != -1 && Grid[SuggestedSolderConnection.Hole1Y][SuggestedSolderConnection.Hole1X] != hole)
            {
                //hole.Invalidate();
                Point relativePosition = this.PointToClient(Cursor.Position);
                Point adjustedRelativePosition = new Point(relativePosition.X - (Grid[SuggestedSolderConnection.Hole1Y][SuggestedSolderConnection.Hole1X].Location.X) - (Preferences.GridSize / 2), relativePosition.Y - (Grid[SuggestedSolderConnection.Hole1Y][SuggestedSolderConnection.Hole1X].Location.Y) - (Preferences.GridSize / 2));
                int x = adjustedRelativePosition.X;
                int y = adjustedRelativePosition.Y;
                if (x == 0 && y == 0)
                {
                    // nothing
                }
                else
                {
                    if (SuggestedSolderConnection.Hole2Y != -1)
                    {
                        Hole tempHole = Grid[SuggestedSolderConnection.Hole2Y][SuggestedSolderConnection.Hole2X];
                        SuggestedSolderConnection.Hole2Y = -1;
                        SuggestedSolderConnection.Hole2X = -1;
                        tempHole.Redraw();

                    }
                    if (((x <= 0) && (y >= 0) && (y > -x)) || ((x >= 0) && (y >= 0) && (y > x)))
                    {
                        // down
                        SuggestedSolderConnection.Hole2Y = Grid[SuggestedSolderConnection.Hole1Y + 1][SuggestedSolderConnection.Hole1X].Position.Y;
                        SuggestedSolderConnection.Hole2X = Grid[SuggestedSolderConnection.Hole1Y + 1][SuggestedSolderConnection.Hole1X].Position.X;
                    }
                    else if (((x >= 0) && (y >= 0) && (x > y)) || ((x >= 0) && (y <= 0) && (x > -y)))
                    {
                        // right
                        SuggestedSolderConnection.Hole2Y = Grid[SuggestedSolderConnection.Hole1Y][SuggestedSolderConnection.Hole1X + 1].Position.Y;
                        SuggestedSolderConnection.Hole2X = Grid[SuggestedSolderConnection.Hole1Y][SuggestedSolderConnection.Hole1X + 1].Position.X;
                    }
                    else if (((x >= 0) && (y <= 0) && (-y > x)) || ((x <= 0) && (y <= 0) && (-y > -x)))
                    {
                        // up
                        SuggestedSolderConnection.Hole2Y = Grid[SuggestedSolderConnection.Hole1Y - 1][SuggestedSolderConnection.Hole1X].Position.Y;
                        SuggestedSolderConnection.Hole2X = Grid[SuggestedSolderConnection.Hole1Y - 1][SuggestedSolderConnection.Hole1X].Position.X;
                    }
                    else if (((x <= 0) && (y <= 0) && (-x > -y)) || ((x <= 0) && (y >= 0) && (-x > y)))
                    {
                        // left
                        SuggestedSolderConnection.Hole2Y = Grid[SuggestedSolderConnection.Hole1Y][SuggestedSolderConnection.Hole1X - 1].Position.Y;
                        SuggestedSolderConnection.Hole2X = Grid[SuggestedSolderConnection.Hole1Y][SuggestedSolderConnection.Hole1X - 1].Position.X;
                    }
                    else
                    {
                        return;
                    }
                    Grid[SuggestedSolderConnection.Hole1Y][SuggestedSolderConnection.Hole1X].Redraw();
                    Grid[SuggestedSolderConnection.Hole2Y][SuggestedSolderConnection.Hole2X].Redraw();
                }
            }

        }

        public void HoleClicked(MouseEventArgs e, Hole hole)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (SelectedTool)
                {
                    case Tool.Solder:
                        {
                            if (!IsHoleCompletelyInsideChip(hole))
                            {
                                if (SuggestedSolderConnection.Hole1Y != -1)
                                {
                                    if (SuggestedSolderConnection.Hole2Y != -1 && hole == Grid[SuggestedSolderConnection.Hole2Y][SuggestedSolderConnection.Hole2X])
                                    {
                                        // make solder connection

                                        Connection newSolderConnection = new Connection();
                                        newSolderConnection.Hole1Y = SuggestedSolderConnection.Hole1Y;
                                        newSolderConnection.Hole1X = SuggestedSolderConnection.Hole1X;
                                        newSolderConnection.Hole2Y = SuggestedSolderConnection.Hole2Y;
                                        newSolderConnection.Hole2X = SuggestedSolderConnection.Hole2X;
                                        newSolderConnection.State = ConnectionState.Placed;

                                        bool alreadyExists = false;
                                        foreach (Connection solderConnection in SolderConnections)
                                        {
                                            if ((Grid[solderConnection.Hole1Y][solderConnection.Hole1X] == Grid[newSolderConnection.Hole1Y][newSolderConnection.Hole1X] && Grid[solderConnection.Hole2Y][solderConnection.Hole2X] == Grid[newSolderConnection.Hole2Y][newSolderConnection.Hole2X])
                                                || (Grid[solderConnection.Hole1Y][solderConnection.Hole1X] == Grid[newSolderConnection.Hole2Y][newSolderConnection.Hole2X] && Grid[solderConnection.Hole2Y][solderConnection.Hole2X] == Grid[newSolderConnection.Hole1Y][newSolderConnection.Hole1X]))
                                            {
                                                // connection already exists
                                                alreadyExists = true;
                                                break;
                                            }
                                        }

                                        if (!alreadyExists)
                                        {
                                            SolderConnections.Add(newSolderConnection);
                                            FormParent.ChangeMade();
                                        }

                                        Grid[SuggestedSolderConnection.Hole1Y][SuggestedSolderConnection.Hole1X].Redraw();
                                        Grid[SuggestedSolderConnection.Hole2Y][SuggestedSolderConnection.Hole2X].Redraw();

                                        SuggestedSolderConnection = new Connection();
                                        FormParent.PlacingFinished();
                                    }
                                }
                                else
                                {
                                    FormParent.PlacingStarted();
                                    SuggestedSolderConnection.Hole1Y = hole.Position.Y;
                                    SuggestedSolderConnection.Hole1X = hole.Position.X;
                                }
                            }
                        }
                        break;
                    case Tool.EraseSolder:
                        {
                            if (!IsHoleCompletelyInsideChip(hole))
                            {
                                if (SuggestedSolderConnection.Hole1Y != -1)
                                {
                                    if (SuggestedSolderConnection.Hole2Y != -1 && hole == Grid[SuggestedSolderConnection.Hole2Y][SuggestedSolderConnection.Hole2X])
                                    {
                                        // delete solder connection
                                        Grid[SuggestedSolderConnection.Hole1Y][SuggestedSolderConnection.Hole1X].Redraw();
                                        Grid[SuggestedSolderConnection.Hole2Y][SuggestedSolderConnection.Hole2X].Redraw();

                                        Connection tempSolderConnection = new Connection();
                                        tempSolderConnection.Hole1Y = SuggestedSolderConnection.Hole1Y;
                                        tempSolderConnection.Hole1X = SuggestedSolderConnection.Hole1X;
                                        tempSolderConnection.Hole2Y = SuggestedSolderConnection.Hole2Y;
                                        tempSolderConnection.Hole2X = SuggestedSolderConnection.Hole2X;

                                        Connection exists = null;
                                        foreach (Connection solderConnection in SolderConnections)
                                        {
                                            if ((Grid[solderConnection.Hole1Y][solderConnection.Hole1X] == Grid[tempSolderConnection.Hole1Y][tempSolderConnection.Hole1X] && Grid[solderConnection.Hole2Y][solderConnection.Hole2X] == Grid[tempSolderConnection.Hole2Y][tempSolderConnection.Hole2X])
                                                || (Grid[solderConnection.Hole1Y][solderConnection.Hole1X] == Grid[tempSolderConnection.Hole2Y][tempSolderConnection.Hole2X] && Grid[solderConnection.Hole2Y][solderConnection.Hole2X] == Grid[tempSolderConnection.Hole1Y][tempSolderConnection.Hole1X]))
                                            {
                                                // connection exists
                                                exists = solderConnection;
                                                break;
                                            }
                                        }
                                        if (exists != null)
                                        {
                                            SolderConnections.Remove(exists);
                                            FormParent.ChangeMade();
                                        }
                                        SuggestedSolderConnection = new Connection();
                                        FormParent.PlacingFinished();
                                    }
                                }
                                else
                                {
                                    FormParent.PlacingStarted();
                                    SuggestedSolderConnection.Hole1Y = hole.Position.Y;
                                    SuggestedSolderConnection.Hole1X = hole.Position.X;
                                    SuggestedSolderConnection.State = ConnectionState.Erasing;
                                }
                            }
                        }
                        break;
                    case Tool.Wire:
                        {
                            if (!IsHoleCompletelyInsideChip(hole))
                            {
                                if (SuggestedWireConnection.Hole1Y != -1)
                                {
                                    // make wire connection

                                    Connection newWireConnection = new Connection();
                                    newWireConnection.Hole1Y = SuggestedWireConnection.Hole1Y;
                                    newWireConnection.Hole1X = SuggestedWireConnection.Hole1X;
                                    newWireConnection.Hole2Y = hole.Position.Y;
                                    newWireConnection.Hole2X = hole.Position.X;
                                    newWireConnection.State = ConnectionState.Placed;

                                    bool alreadyExists = false;
                                    foreach (Connection wireConnection in WireConnections)
                                    {
                                        if ((Grid[wireConnection.Hole1Y][wireConnection.Hole1X] == Grid[newWireConnection.Hole1Y][newWireConnection.Hole1X] || Grid[wireConnection.Hole2Y][wireConnection.Hole2X] == Grid[newWireConnection.Hole2Y][newWireConnection.Hole2X])
                                            || (Grid[wireConnection.Hole1Y][wireConnection.Hole1X] == Grid[newWireConnection.Hole2Y][newWireConnection.Hole2X] || Grid[wireConnection.Hole2Y][wireConnection.Hole2X] == Grid[newWireConnection.Hole1Y][newWireConnection.Hole1X]))
                                        {
                                            // connection already exists
                                            alreadyExists = true;
                                            break;
                                        }
                                    }

                                    if (!alreadyExists)
                                    {
                                        WireConnections.Add(newWireConnection);
                                        FormParent.ChangeMade();
                                    }

                                    // Redraw all squares between the two
                                    int minY;
                                    int maxY;
                                    if (newWireConnection.Hole1Y <= newWireConnection.Hole2Y)
                                    {
                                        minY = newWireConnection.Hole1Y;
                                        maxY = newWireConnection.Hole2Y;
                                    }
                                    else
                                    {
                                        minY = newWireConnection.Hole2Y;
                                        maxY = newWireConnection.Hole1Y;
                                    }
                                    int minX;
                                    int maxX;
                                    if (newWireConnection.Hole1X <= newWireConnection.Hole2X)
                                    {
                                        minX = newWireConnection.Hole1X;
                                        maxX = newWireConnection.Hole2X;
                                    }
                                    else
                                    {
                                        minX = newWireConnection.Hole2X;
                                        maxX = newWireConnection.Hole1X;
                                    }
                                    for (int y = minY; y <= maxY; y++)
                                    {
                                        for (int x = minX; x <= maxX; x++)
                                        {
                                            Grid[y][x].Redraw();
                                        }
                                    }

                                    SuggestedWireConnection = new Connection();
                                    FormParent.PlacingFinished();
                                }
                                else
                                {
                                    bool alreadyExists = false;
                                    foreach (Connection wireConnection in WireConnections)
                                    {
                                        if ((wireConnection.Hole1Y == hole.Position.Y && wireConnection.Hole1X == hole.Position.X)
                                            || wireConnection.Hole2Y == hole.Position.Y && wireConnection.Hole2X == hole.Position.X)
                                        {
                                            alreadyExists = true;
                                            break;
                                        }
                                    }
                                    if (!alreadyExists)
                                    {
                                        FormParent.PlacingStarted();
                                        SuggestedWireConnection.Hole1Y = hole.Position.Y;
                                        SuggestedWireConnection.Hole1X = hole.Position.X;
                                    }
                                }
                            }
                        }
                        break;
                    case Tool.EraseWire:
                        {
                            if (!IsHoleCompletelyInsideChip(hole))
                            {
                                if (SuggestedWireConnection.Hole1Y != -1)
                                {
                                    // delete wire connection

                                    Connection tempWireConnection = new Connection();
                                    tempWireConnection.Hole1Y = SuggestedWireConnection.Hole1Y;
                                    tempWireConnection.Hole1X = SuggestedWireConnection.Hole1X;
                                    tempWireConnection.Hole2Y = hole.Position.Y;
                                    tempWireConnection.Hole2X = hole.Position.X;

                                    Connection exists = null;
                                    foreach (Connection wireConnection in WireConnections)
                                    {
                                        if ((Grid[wireConnection.Hole1Y][wireConnection.Hole1X] == Grid[tempWireConnection.Hole1Y][tempWireConnection.Hole1X] && Grid[wireConnection.Hole2Y][wireConnection.Hole2X] == Grid[tempWireConnection.Hole2Y][tempWireConnection.Hole2X])
                                            || (Grid[wireConnection.Hole1Y][wireConnection.Hole1X] == Grid[tempWireConnection.Hole2Y][tempWireConnection.Hole2X] && Grid[wireConnection.Hole2Y][wireConnection.Hole2X] == Grid[tempWireConnection.Hole1Y][tempWireConnection.Hole1X]))
                                        {
                                            // connection exists
                                            exists = wireConnection;
                                            break;
                                        }
                                    }
                                    if (exists != null)
                                    {
                                        WireConnections.Remove(exists);
                                        FormParent.ChangeMade();
                                    }


                                    // Redraw all squares between the two
                                    int minY;
                                    int maxY;
                                    if (tempWireConnection.Hole1Y <= tempWireConnection.Hole2Y)
                                    {
                                        minY = tempWireConnection.Hole1Y;
                                        maxY = tempWireConnection.Hole2Y;
                                    }
                                    else
                                    {
                                        minY = tempWireConnection.Hole2Y;
                                        maxY = tempWireConnection.Hole1Y;
                                    }
                                    int minX;
                                    int maxX;
                                    if (tempWireConnection.Hole1X <= tempWireConnection.Hole2X)
                                    {
                                        minX = tempWireConnection.Hole1X;
                                        maxX = tempWireConnection.Hole2X;
                                    }
                                    else
                                    {
                                        minX = tempWireConnection.Hole2X;
                                        maxX = tempWireConnection.Hole1X;
                                    }
                                    for (int y = minY; y <= maxY; y++)
                                    {
                                        for (int x = minX; x <= maxX; x++)
                                        {
                                            Grid[y][x].Redraw();
                                        }
                                    }

                                    SuggestedWireConnection = new Connection();
                                    FormParent.PlacingFinished();
                                }
                                else
                                {
                                    FormParent.PlacingStarted();
                                    SuggestedWireConnection.Hole1Y = hole.Position.Y;
                                    SuggestedWireConnection.Hole1X = hole.Position.X;
                                    SuggestedWireConnection.State = ConnectionState.Erasing;
                                }
                            }
                        }
                        break;
                    case Tool.Chip:
                        {
                            if (SuggestedChip.TopLeftHoleY != -1)
                            {
                                if (Grid[SuggestedChip.TopLeftHoleY][SuggestedChip.TopLeftHoleX] != hole)
                                {
                                    Hole tempHole1 = Grid[SuggestedChip.TopLeftHoleY][SuggestedChip.TopLeftHoleX];
                                    Hole tempHole2 = hole;

                                    Chip newChip = new Chip();

                                    if (tempHole1.Position.X <= tempHole2.Position.X && tempHole1.Position.Y <= tempHole2.Position.Y)
                                    {
                                        newChip.TopLeftHoleY = tempHole1.Position.Y;
                                        newChip.TopLeftHoleX = tempHole1.Position.X;
                                        newChip.BottomRightHoleY = tempHole2.Position.Y;
                                        newChip.BottomRightHoleX = tempHole2.Position.X;
                                    }
                                    else if (tempHole1.Position.X >= tempHole2.Position.X && tempHole1.Position.Y >= tempHole2.Position.Y)
                                    {
                                        newChip.TopLeftHoleY = tempHole2.Position.Y;
                                        newChip.TopLeftHoleX = tempHole2.Position.X;
                                        newChip.BottomRightHoleY = tempHole1.Position.Y;
                                        newChip.BottomRightHoleX = tempHole1.Position.X;
                                    }
                                    else if (tempHole1.Position.X <= tempHole2.Position.X && tempHole1.Position.Y >= tempHole2.Position.Y)
                                    {
                                        newChip.TopLeftHoleY = tempHole2.Position.Y;
                                        newChip.TopLeftHoleX = tempHole1.Position.X;
                                        newChip.BottomRightHoleY = tempHole1.Position.Y;
                                        newChip.BottomRightHoleX = tempHole2.Position.X;
                                    }
                                    else if (tempHole1.Position.X >= tempHole2.Position.X && tempHole1.Position.Y <= tempHole2.Position.Y)
                                    {
                                        newChip.TopLeftHoleY = tempHole1.Position.Y;
                                        newChip.TopLeftHoleX = tempHole2.Position.X;
                                        newChip.BottomRightHoleY = tempHole2.Position.Y;
                                        newChip.BottomRightHoleX = tempHole1.Position.X;
                                    }
                                    else
                                    {
                                        return; // should never be here
                                    }

                                    if (newChip.BottomRightHoleX - newChip.TopLeftHoleX <= newChip.BottomRightHoleY - newChip.TopLeftHoleY)
                                    {
                                        newChip.Orientation = Orientation.Up;
                                    }
                                    else
                                    {
                                        newChip.Orientation = Orientation.Right;
                                    }


                                    // check if overlap other chips
                                    bool overlap = false;

                                    int orientationAddY = 0;
                                    int orientationAddX = 0;
                                    if (newChip.Orientation == Orientation.Up || newChip.Orientation == Orientation.Down)
                                    {
                                        orientationAddY = 1;
                                    }
                                    else
                                    {
                                        orientationAddX = 1;
                                    }

                                    for (int y = newChip.TopLeftHoleY - orientationAddY; y <= newChip.BottomRightHoleY + orientationAddY; y++)
                                    {
                                        for (int x = newChip.TopLeftHoleX - orientationAddX; x <= newChip.BottomRightHoleX + orientationAddX; x++)
                                        {
                                            if (y >= 0 && y < Preferences.GridHeight && x >= 0 && x < Preferences.GridWidth)
                                            {
                                                // check if overlap other chips
                                                foreach (Chip otherChip in Chips)
                                                {
                                                    for (int otherChipY = otherChip.TopLeftHoleY; otherChipY <= otherChip.BottomRightHoleY; otherChipY++)
                                                    {
                                                        for (int otherChipX = otherChip.TopLeftHoleX; otherChipX <= otherChip.BottomRightHoleX; otherChipX++)
                                                        {
                                                            if (y == otherChipY && x == otherChipX)
                                                            {
                                                                // chip overlaps
                                                                overlap = true;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (!overlap)
                                    {
                                        newChip.Name = Microsoft.VisualBasic.Interaction.InputBox("Please enter a chip name:", "Chip Name", "", -1, -1);

                                        Chips.Add(newChip);
                                        FormParent.ChangeMade();

                                        for (int y = newChip.TopLeftHoleY - orientationAddY; y <= newChip.BottomRightHoleY + orientationAddY; y++)
                                        {
                                            for (int x = newChip.TopLeftHoleX - orientationAddX; x <= newChip.BottomRightHoleX + orientationAddX; x++)
                                            {
                                                if (y >= 0 && y < Preferences.GridHeight && x >= 0 && x < Preferences.GridWidth)
                                                {
                                                    Grid[y][x].Redraw();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // overlap
                                        Grid[SuggestedChip.TopLeftHoleY][SuggestedChip.TopLeftHoleX].Redraw();
                                    }

                                    SuggestedChip = new Chip();
                                    FormParent.PlacingFinished();
                                }
                            }
                            else
                            {
                                FormParent.PlacingStarted();
                                SuggestedChip.TopLeftHoleY = hole.Position.Y;
                                SuggestedChip.TopLeftHoleX = hole.Position.X;
                                hole.Redraw();
                            }
                        }
                        break;
                    case Tool.RotateChip:
                        {
                            Chip chipUnderMouse = FindChipOverHole(hole);
                            if (chipUnderMouse != null)
                            {
                                switch (chipUnderMouse.Orientation)
                                {
                                    case Orientation.Up:
                                        chipUnderMouse.Orientation = Orientation.Right;
                                        break;
                                    case Orientation.Right:
                                        chipUnderMouse.Orientation = Orientation.Down;
                                        break;
                                    case Orientation.Down:
                                        chipUnderMouse.Orientation = Orientation.Left;
                                        break;
                                    case Orientation.Left:
                                        chipUnderMouse.Orientation = Orientation.Up;
                                        break;
                                }
                                FormParent.ChangeMade();
                                for (int y = chipUnderMouse.TopLeftHoleY - 1; y <= chipUnderMouse.BottomRightHoleY + 1; y++)
                                {
                                    for (int x = chipUnderMouse.TopLeftHoleX - 1; x <= chipUnderMouse.BottomRightHoleX + 1; x++)
                                    {
                                        if (y >= 0 && y < Preferences.GridHeight && x >= 0 && x < Preferences.GridWidth)
                                        {
                                            Grid[y][x].Redraw();
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case Tool.EraseChip:
                        {
                            Chip chipUnderMouse = FindChipOverHole(hole);
                            if (chipUnderMouse != null)
                            {
                                Chips.Remove(chipUnderMouse);
                                FormParent.ChangeMade();
                                int orientationAddY = 0;
                                int orientationAddX = 0;
                                if (chipUnderMouse.Orientation == Orientation.Up || chipUnderMouse.Orientation == Orientation.Down)
                                {
                                    orientationAddY = 1;
                                }
                                else
                                {
                                    orientationAddX = 1;
                                }
                                for (int y = chipUnderMouse.TopLeftHoleY - orientationAddY; y <= chipUnderMouse.BottomRightHoleY + orientationAddY; y++)
                                {
                                    for (int x = chipUnderMouse.TopLeftHoleX - orientationAddX; x <= chipUnderMouse.BottomRightHoleX + orientationAddX; x++)
                                    {
                                        if (y >= 0 && y < Preferences.GridHeight && x >= 0 && x < Preferences.GridWidth)
                                        {
                                            Grid[y][x].Redraw();
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }

        private bool IsHoleCompletelyInsideChip(Hole hole)
        {
            bool holeInsideChip = false;
            foreach (Chip chip in Chips)
            {
                int orientationAddY = 0;
                int orientationAddX = 0;
                if (chip.Orientation == Orientation.Up || chip.Orientation == Orientation.Down)
                {
                    orientationAddX = 1;
                }
                else
                {
                    orientationAddY = 1;
                }
                // X and Y are flipped for this one as we want to allow solder from the side pins
                //////////////////////////
                // DO NOT COPY THIS ONE //
                //////////////////////////
                for (int y = chip.TopLeftHoleY + orientationAddY; y <= chip.BottomRightHoleY - orientationAddY; y++)
                {
                    for (int x = chip.TopLeftHoleX + orientationAddX; x <= chip.BottomRightHoleX - orientationAddX; x++)
                    {
                        if (y == hole.Position.Y && x == hole.Position.X)
                        {
                            // chip overlaps
                            holeInsideChip = true;
                            break;
                        }
                    }
                }
            }
            return holeInsideChip;
        }

        private Chip FindChipOverHole(Hole hole)
        {
            Chip chipOverHole = null;
            foreach (Chip chip in Chips)
            {
                for (int y = chip.TopLeftHoleY; y <= chip.BottomRightHoleY; y++)
                {
                    for (int x = chip.TopLeftHoleX; x <= chip.BottomRightHoleX; x++)
                    {
                        if (y == hole.Position.Y && x == hole.Position.X)
                        {
                            // chip overlaps
                            chipOverHole = chip;
                            break;
                        }
                    }
                }
            }
            return chipOverHole;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);


            using (Pen blackPen = new Pen(Color.Black, 2))
            using (Pen wirePen = new Pen(Color.OrangeRed, Preferences.HoleSize / 4))
            using (SolidBrush hoverBrush = new SolidBrush(Color.LawnGreen))
            using (SolidBrush pressedBrush = new SolidBrush(Color.LimeGreen))
            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            using (SolidBrush blackBrush = new SolidBrush(Color.Black))
            using (SolidBrush indicatorBrush = new SolidBrush(Color.LightGray))
            using (SolidBrush solderBrush = new SolidBrush(Color.Gray))
            using (SolidBrush suggestedSolderBrush = new SolidBrush(Color.LimeGreen))
            using (SolidBrush erasingSolderBrush = new SolidBrush(Color.OrangeRed))
            using (SolidBrush suggestedChipBrush = new SolidBrush(Color.LimeGreen))
            using (SolidBrush chipBrush = new SolidBrush(Color.Orange))
            using (Font chipFont = new Font("Courier New", 20))
            {
                for (int y = 0; y < Grid.Length; y++)
                {
                    for (int x = 0; x < Grid[0].Length; x++)
                    {
                        Hole hole = Grid[y][x];
                        Rectangle square = new Rectangle(new Point((x * Preferences.GridSize), (y * Preferences.GridSize)), new Size(Preferences.GridSize, Preferences.GridSize));
                        Rectangle outline = new Rectangle(new Point((x * Preferences.GridSize) + Preferences.Offset, (y * Preferences.GridSize) + Preferences.Offset), new Size(Preferences.HoleSize, Preferences.HoleSize));

                        Rectangle rightSolderRect = new Rectangle(new Point((x * Preferences.GridSize) + Preferences.Offset / 2, (y * Preferences.GridSize) + Preferences.Offset / 2), new Size(Preferences.HoleSize + Preferences.Offset + Preferences.Offset, Preferences.HoleSize + Preferences.Offset));
                        Rectangle leftSolderRect = new Rectangle(new Point((x * Preferences.GridSize) + 0, (y * Preferences.GridSize) + Preferences.Offset / 2), new Size(Preferences.Offset + Preferences.HoleSize + Preferences.Offset / 2, Preferences.HoleSize + Preferences.Offset));
                        Rectangle downSolderRect = new Rectangle(new Point((x * Preferences.GridSize) + Preferences.Offset / 2, (y * Preferences.GridSize) + Preferences.Offset / 2), new Size(Preferences.HoleSize + Preferences.Offset, Preferences.Offset / 2 + Preferences.HoleSize + Preferences.Offset));
                        Rectangle upSolderRect = new Rectangle(new Point((x * Preferences.GridSize) + Preferences.Offset / 2, (y * Preferences.GridSize) + 0), new Size(Preferences.HoleSize + Preferences.Offset, Preferences.Offset + Preferences.HoleSize + Preferences.Offset / 2));

                        foreach (Connection solderConnection in SolderConnections)
                        {
                            Hole otherSolderHole = null;
                            if (solderConnection.Hole1Y != -1)
                            {
                                if (Grid[solderConnection.Hole1Y][solderConnection.Hole1X] == hole)
                                {
                                    otherSolderHole = Grid[solderConnection.Hole2Y][solderConnection.Hole2X];
                                }
                            }
                            if (solderConnection.Hole2Y != -1)
                            {
                                if (Grid[solderConnection.Hole2Y][solderConnection.Hole2X] == hole)
                                {
                                    otherSolderHole = Grid[solderConnection.Hole1Y][solderConnection.Hole1X];
                                }
                            }
                            if (otherSolderHole != null)
                            {
                                if (otherSolderHole.Position.X > hole.Position.X)
                                {
                                    // right
                                    pe.Graphics.FillRectangle(solderBrush, rightSolderRect);
                                }
                                else if (otherSolderHole.Position.X < hole.Position.X)
                                {
                                    // left
                                    pe.Graphics.FillRectangle(solderBrush, leftSolderRect);
                                }
                                else if (otherSolderHole.Position.Y > hole.Position.Y)
                                {
                                    // down
                                    pe.Graphics.FillRectangle(solderBrush, downSolderRect);
                                }
                                else if (otherSolderHole.Position.Y < hole.Position.Y)
                                {
                                    // up
                                    pe.Graphics.FillRectangle(solderBrush, upSolderRect);
                                }
                            }
                        }

                        Hole otherSuggestedSolderHole = null;
                        if (SuggestedSolderConnection.Hole1Y != -1 && SuggestedSolderConnection.Hole2Y != -1)
                        {
                            if (Grid[SuggestedSolderConnection.Hole1Y][SuggestedSolderConnection.Hole1X] == hole)
                            {
                                otherSuggestedSolderHole = Grid[SuggestedSolderConnection.Hole2Y][SuggestedSolderConnection.Hole2X];
                            }
                        }
                        if (SuggestedSolderConnection.Hole1Y != -1 && SuggestedSolderConnection.Hole2Y != -1)
                        {
                            if (Grid[SuggestedSolderConnection.Hole2Y][SuggestedSolderConnection.Hole2X] == hole)
                            {
                                otherSuggestedSolderHole = Grid[SuggestedSolderConnection.Hole1Y][SuggestedSolderConnection.Hole1X];
                            }
                        }
                        if (otherSuggestedSolderHole != null)
                        {
                            SolidBrush brushToUse = null;
                            if (SuggestedSolderConnection.State == ConnectionState.Suggested)
                            {
                                brushToUse = suggestedSolderBrush;
                            }
                            else
                            {
                                brushToUse = erasingSolderBrush;
                            }

                            if (otherSuggestedSolderHole.Position.X > hole.Position.X)
                            {
                                // right
                                pe.Graphics.FillRectangle(brushToUse, rightSolderRect);
                            }
                            else if (otherSuggestedSolderHole.Position.X < hole.Position.X)
                            {
                                // left
                                pe.Graphics.FillRectangle(brushToUse, leftSolderRect);
                            }
                            else if (otherSuggestedSolderHole.Position.Y > hole.Position.Y)
                            {
                                // down
                                pe.Graphics.FillRectangle(brushToUse, downSolderRect);
                            }
                            else if (otherSuggestedSolderHole.Position.Y < hole.Position.Y)
                            {
                                // up
                                pe.Graphics.FillRectangle(brushToUse, upSolderRect);
                            }
                        }

                        if (SuggestedChip.TopLeftHoleY != -1)
                        {
                            if (Grid[SuggestedChip.TopLeftHoleY][SuggestedChip.TopLeftHoleX] == hole)
                            {
                                pe.Graphics.FillRectangle(suggestedChipBrush, square);
                            }
                        }

                        if (hole.Hover)
                        {
                            if (MousePressed)
                            {
                                pe.Graphics.FillRectangle(pressedBrush, outline);
                            }
                            else
                            {
                                pe.Graphics.FillRectangle(hoverBrush, outline);
                            }
                        }
                        else
                        {
                            pe.Graphics.FillRectangle(whiteBrush, outline);
                        }

                        if (SuggestedWireConnection.Hole1Y != -1)
                        {

                            Rectangle innerSquare = new Rectangle(new Point(outline.Location.X + Preferences.HoleSize / 5, outline.Location.Y + Preferences.HoleSize / 5), new Size(Preferences.HoleSize - 2 * (Preferences.HoleSize / 5), Preferences.HoleSize - 2 * (Preferences.HoleSize / 5)));

                            if (Grid[SuggestedWireConnection.Hole1Y][SuggestedWireConnection.Hole1X] == hole)
                            {
                                pe.Graphics.FillRectangle(suggestedSolderBrush, innerSquare);
                            }
                        }

                        pe.Graphics.DrawRectangle(blackPen, outline);
                    }
                }

                foreach (Chip chip in Chips)
                {
                    Rectangle outline = new Rectangle();
                    Rectangle indicator = new Rectangle();
                    switch (chip.Orientation)
                    {
                        case Orientation.Up:
                            {
                                outline = new Rectangle(new Point(Grid[chip.TopLeftHoleY][chip.TopLeftHoleX].Location.X + (Preferences.GridSize / 3), Grid[chip.TopLeftHoleY][chip.TopLeftHoleX].Location.Y - (Preferences.GridSize / 3)),
                                    new Size((((chip.BottomRightHoleX - chip.TopLeftHoleX) + 1) * Preferences.GridSize) - (2 * (Preferences.GridSize / 3)), (((chip.BottomRightHoleY - chip.TopLeftHoleY) + 1) * Preferences.GridSize) + (2 * (Preferences.GridSize / 3))));

                                indicator = new Rectangle(new Point(outline.Location.X + (outline.Width / 2) - (outline.Width / 10), outline.Location.Y), new Size(2 * (outline.Width / 10), (outline.Height / 20)));
                            }
                            break;
                        case Orientation.Right:
                            {
                                outline = new Rectangle(new Point(Grid[chip.TopLeftHoleY][chip.TopLeftHoleX].Location.X - (Preferences.GridSize / 3), Grid[chip.TopLeftHoleY][chip.TopLeftHoleX].Location.Y + (Preferences.GridSize / 3)),
                                    new Size((((chip.BottomRightHoleX - chip.TopLeftHoleX) + 1) * Preferences.GridSize) + (2 * (Preferences.GridSize / 3)), (((chip.BottomRightHoleY - chip.TopLeftHoleY) + 1) * Preferences.GridSize) - (2 * (Preferences.GridSize / 3))));

                                indicator = new Rectangle(new Point(outline.Location.X + (outline.Width) - (outline.Width / 20), outline.Location.Y + (outline.Height / 2) - (outline.Height / 10)), new Size((outline.Width / 20), 2 * (outline.Height / 10)));
                            }
                            break;
                        case Orientation.Down:
                            {
                                outline = new Rectangle(new Point(Grid[chip.TopLeftHoleY][chip.TopLeftHoleX].Location.X + (Preferences.GridSize / 3), Grid[chip.TopLeftHoleY][chip.TopLeftHoleX].Location.Y - (Preferences.GridSize / 3)),
                                    new Size((((chip.BottomRightHoleX - chip.TopLeftHoleX) + 1) * Preferences.GridSize) - (2 * (Preferences.GridSize / 3)), (((chip.BottomRightHoleY - chip.TopLeftHoleY) + 1) * Preferences.GridSize) + (2 * (Preferences.GridSize / 3))));

                                indicator = new Rectangle(new Point(outline.Location.X + (outline.Width / 2) - (outline.Width / 10), outline.Location.Y + outline.Height - (outline.Height / 20)), new Size(2 * (outline.Width / 10), (outline.Height / 20)));
                            }
                            break;
                        case Orientation.Left:
                            {
                                outline = new Rectangle(new Point(Grid[chip.TopLeftHoleY][chip.TopLeftHoleX].Location.X - (Preferences.GridSize / 3), Grid[chip.TopLeftHoleY][chip.TopLeftHoleX].Location.Y + (Preferences.GridSize / 3)),
                                    new Size((((chip.BottomRightHoleX - chip.TopLeftHoleX) + 1) * Preferences.GridSize) + (2 * (Preferences.GridSize / 3)), (((chip.BottomRightHoleY - chip.TopLeftHoleY) + 1) * Preferences.GridSize) - (2 * (Preferences.GridSize / 3))));

                                indicator = new Rectangle(new Point(outline.Location.X, outline.Location.Y + (outline.Height / 2) - (outline.Height / 10)), new Size((outline.Width / 20), 2 * (outline.Height / 10)));
                            }
                            break;
                    }

                    pe.Graphics.FillRectangle(whiteBrush, outline);
                    pe.Graphics.FillRectangle(indicatorBrush, indicator);
                    pe.Graphics.DrawRectangle(blackPen, outline);
                    pe.Graphics.DrawRectangle(blackPen, indicator);


                    SizeF size = pe.Graphics.MeasureString(chip.Name, chipFont);

                    switch (chip.Orientation)
                    {
                        case Orientation.Up:
                            {
                                pe.Graphics.RotateTransform(90);
                                pe.Graphics.TranslateTransform(outline.Location.X + outline.Location.Y + size.Height / 2 + outline.Width / 2, outline.Location.Y - outline.Location.X + outline.Height / 2 - size.Width / 2, System.Drawing.Drawing2D.MatrixOrder.Append);
                            }
                            break;
                        case Orientation.Right:
                            {
                                pe.Graphics.TranslateTransform(outline.Width / 2 - size.Width / 2, outline.Height / 2 - size.Height / 2, System.Drawing.Drawing2D.MatrixOrder.Append);
                            }
                            break;
                        case Orientation.Down:
                            {
                                pe.Graphics.RotateTransform(90);
                                pe.Graphics.TranslateTransform(outline.Location.X + outline.Location.Y + size.Height / 2 + outline.Width / 2, outline.Location.Y - outline.Location.X + outline.Height / 2 - size.Width / 2, System.Drawing.Drawing2D.MatrixOrder.Append);
                            }
                            break;
                        case Orientation.Left:
                            {
                                pe.Graphics.TranslateTransform(outline.Width / 2 - size.Width / 2, outline.Height / 2 - size.Height / 2, System.Drawing.Drawing2D.MatrixOrder.Append);
                            }
                            break;
                    }

                    pe.Graphics.DrawString(chip.Name, chipFont, blackBrush, outline.Location.X, outline.Location.Y);
                    pe.Graphics.ResetTransform();
                }



                foreach (Connection wireConnection in WireConnections)
                {
                    Point grid1 = new Point((wireConnection.Hole1X * Preferences.GridSize), (wireConnection.Hole1Y * Preferences.GridSize));
                    Rectangle innerSquare1 = new Rectangle(new Point(grid1.X + Preferences.Offset + (Preferences.HoleSize / 5), grid1.Y + Preferences.Offset + (Preferences.HoleSize / 5)),
                        new Size(Preferences.HoleSize - 2 * (Preferences.HoleSize / 5), Preferences.HoleSize - 2 * (Preferences.HoleSize / 5)));
                    Point innerSquare1Centre = new Point(grid1.X + (Preferences.GridSize / 2), grid1.Y + (Preferences.GridSize / 2));

                    Point grid2 = new Point((wireConnection.Hole2X * Preferences.GridSize), (wireConnection.Hole2Y * Preferences.GridSize));
                    Rectangle innerSquare2 = new Rectangle(new Point(grid2.X + Preferences.Offset + (Preferences.HoleSize / 5), grid2.Y + Preferences.Offset + (Preferences.HoleSize / 5)),
                        new Size(Preferences.HoleSize - 2 * (Preferences.HoleSize / 5), Preferences.HoleSize - 2 * (Preferences.HoleSize / 5)));
                    Point innerSquare2Centre = new Point(grid2.X + (Preferences.GridSize / 2), grid2.Y + (Preferences.GridSize / 2));

                    pe.Graphics.DrawLine(wirePen, innerSquare1Centre, innerSquare2Centre);
                    pe.Graphics.FillRectangle(solderBrush, innerSquare1);
                    pe.Graphics.FillRectangle(solderBrush, innerSquare2);

                }
            }

        }
    }

    public class Hole
    {
        GridContainer Parent;

        private Point position;

        public Point Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                this.Location = new Point((int)(value.X * Preferences.GridSize), (int)(value.Y * Preferences.GridSize));
            }
        }

        private Point location;

        public Point Location
        {
            get
            {
                return location;
            }
            private set
            {
                location = value;
            }
        }

        public bool Hover;

        public delegate void HoleClicked(MouseEventArgs e, Hole hole);
        public event HoleClicked OnMouseClickExt;

        public delegate void HoleHovered(Hole hole);
        public event HoleHovered OnHoleHover;

        public Hole(GridContainer parent, int x, int y)
        {
            Parent = parent;
            Hover = false;
            Position = new Point(x, y);
        }

        public void OnMouseEnter()
        {
            if (OnHoleHover != null)
                OnHoleHover(this);
            Hover = true;
            Redraw();
        }

        public void OnMouseLeave()
        {
            Hover = false;
            Redraw();
        }

        public void OnMouseClick(MouseEventArgs e)
        {
            if (OnMouseClickExt != null)
                OnMouseClickExt(e, this);
        }

        public void Redraw()
        {
            Rectangle area = new Rectangle(Location, new Size(Preferences.GridSize, Preferences.GridSize));
            Parent.Invalidate(area);
        }
    }

    public enum ConnectionState
    {
        Suggested,
        Placed,
        Erasing,
    }

    public enum Orientation
    {
        Up,
        Right,
        Down,
        Left,
    }

    public enum Tool
    {
        Solder,
        EraseSolder,
        Wire,
        EraseWire,
        Chip,
        RotateChip,
        EraseChip,
    }

    [Serializable]
    public class Connection
    {
        public int Hole1X;
        public int Hole1Y;
        public int Hole2X;
        public int Hole2Y;
        public ConnectionState State;

        public Connection()
        {
            Hole1X = -1;
            Hole1Y = -1;
            Hole2X = -1;
            Hole2Y = -1;
            State = ConnectionState.Suggested;
        }
    }

    [Serializable]
    public class Chip
    {
        public int TopLeftHoleX;
        public int TopLeftHoleY;
        public int BottomRightHoleX;
        public int BottomRightHoleY;
        public ConnectionState State;
        public string Name;
        public Orientation Orientation;

        public Chip()
        {
            TopLeftHoleX = -1;
            TopLeftHoleY = -1;
            BottomRightHoleX = -1;
            BottomRightHoleY = -1;
            State = ConnectionState.Suggested;
            Orientation = Orientation.Up;
        }
    }

    public static class Preferences
    {
        public const int GridSize = 32;
        public const int HoleSize = 20;
        public const int Offset = (GridSize - HoleSize) / 2;

        public static int GridHeight;
        public static int GridWidth;
    }
}
