using System;
using System.IO;  // Path.Combine을 사용하기 위해 필요
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Drawing;  // Color 사용을 위해 추가
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;

namespace bibleViewer
{
    public partial class Form1 : Form
    {


        // DB주소 저장.
        // private string connectionString = "Data Source=C:\\Users\\82104\\Desktop\\잡동사니\\바이블뷰어\\bibleViewer\\bibleViewer\\bin\Debug\net7.0-windows\\data\\개역개정.bdb;Version=3;";
        private string connectionString;

        private List<string> originalItems = new List<string>();

        public string referenceBookId = "";
        public string referenceChapter = "";
        public string referenceVerse = "";
        public string referenceVerse2 = "";

        public Form1()
        {
            InitializeComponent();

            string dbPath = Path.Combine(Application.StartupPath, "data", "개역개정.bdb");
            connectionString = $"Data Source={dbPath};Version=3;";
            this.Text = "BibleViewer";
            this.Load += new System.EventHandler(this.itemBox_Load); // Load 이벤트 연결
            this.KeyPreview = true;  // 키 이벤트를 폼에서 먼저 받음
            this.KeyDown += new KeyEventHandler(textBox1_KeyDown);
        }

        //// 데이터 읽어오기.
        private void FetchData(int bookId, int chapter, int verse, int verse2)
        {

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                string query = "SELECT verse, btext FROM Bible WHERE book = @book AND chapter = @chapter AND verse BETWEEN @verse AND @verse2 ORDER BY verse";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // 매개변수 추가
                    command.Parameters.AddWithValue("@book", bookId);
                    command.Parameters.AddWithValue("@chapter", chapter);
                    command.Parameters.AddWithValue("@verse", verse);
                    command.Parameters.AddWithValue("@verse2", verse2);

                    try
                    {
                        connection.Open(); // 데이터베이스 연결 열기
                        using (SQLiteDataReader reader = command.ExecuteReader()) // 쿼리 실행
                        {
                            if (!reader.HasRows)
                            {
                                MessageBox.Show("구절을 찾을 수 없습니다.");
                                return;
                            }
                            resultDataGridView.Columns.Clear();
                            resultDataGridView.Rows.Clear();

                            // 컬럼 추가
                            resultDataGridView.Columns.Add("btext", "성경 구절");


                            while (reader.Read()) // 여러 개의 구절 읽기
                            {
                                int verseNum = reader.GetInt32(0); // 절 번호
                                string verseText = reader.GetString(1); // 구절 내용
                                resultDataGridView.Rows.Add($"{verseNum}. {verseText}");
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("쿼리 실행 실패: " + ex.Message);
                    }
                }
            }
        }
        // 검색버튼 누르면.
        private void btnStart_Click(object sender, EventArgs e)
        {
            // 선택된 값 가져오기
            // int bookId = itemBox.SelectedIndex + 1; // ComboBox의 인덱스는 0부터 시작하므로 1을 더함
            int bookId = 0;
            for (int i = 0; i < originalItems.Count; i++)
            {
                if (originalItems[i] == itemBox.Text)
                {
                    bookId = i + 1;
                    break;
                }
            }
            // int chapter = Convert.ToInt32(chapterBox.SelectedItem);
            int chapter = Convert.ToInt32(chapterBox.Text);
            //int verse = Convert.ToInt32(verseBox.SelectedItem);
            int verse = Convert.ToInt32(verseBox.Text);
            //int verse2 = Convert.ToInt32(verseBox2.SelectedItem);
            int verse2 = Convert.ToInt32(verseBox2.Text);

            string reference = "";

            if (verse == verse2)
            {
                referenceBookId = itemBox.Text;
                referenceChapter = chapterBox.Text;
                referenceVerse = verseBox.Text;
                referenceVerse2 = "";
            }
            else
            {
                referenceBookId = itemBox.Text;
                referenceChapter = chapterBox.Text;
                referenceVerse = verseBox.Text;
                referenceVerse2 = verseBox2.Text;
            }

            // 구절 가져오기
            FetchData(bookId, chapter, verse, verse2);
        }

        private void itemBox_Load(object sender, EventArgs e)
        {
            originalItems.AddRange(new string[]
            {
                "창세기", "출애굽기", "레위기", "민수기", "신명기",
                "여호수아", "사사기", "룻기", "사무엘상", "사무엘하",
                "열왕기상", "열왕기하", "역대상", "역대하", "에스라",
                "느헤미야", "에스더", "욥기", "시편", "잠언", "전도서",
                "아가", "이사야", "예레미야", "예레미야 애가", "에스겔",
                "다니엘", "호세아", "요엘", "아모스", "오바댜", "요나",
                "미가", "나훔", "하박국", "스바냐", "학개", "스가랴",
                "말라기", "마태복음", "마가복음", "누가복음", "요한복음",
                "사도행전", "로마서", "고린도전서", "고린도후서", "갈라디아서",
                "에베소서", "빌립보서", "골로새서", "데살로니가전서", "데살로니가후서",
                "디모데전서", "디모데후서", "디도서", "빌레몬서", "히브리서",
                "야고보서", "베드로전서", "베드로후서", "요한일서", "요한이서",
                "요한삼서", "유다서", "요한계시록"
            });

            // 초기 아이템 설정
            itemBox.Items.AddRange(originalItems.ToArray());
        }

        private void itemBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            chapterBox.Items.Clear(); // 기존에 있던 장 데이터 삭제.
            verseBox.Items.Clear();// 기존에 있던 절 데이터 삭제
            verseBox2.Items.Clear();// 기존에 있던 절 데이터 삭제

            int bookId = itemBox.SelectedIndex + 1;
            LoadChapters(bookId);
        }
        private void LoadChapters(int bookId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // Bible테이블에서 
                string query = "SELECT DISTINCT chapter FROM Bible WHERE book = @book ORDER BY chapter";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@book", bookId);

                try
                {
                    connection.Open();
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int chapter = Convert.ToInt32(reader["chapter"]);
                        chapterBox.Items.Add(chapter); // 장 추가
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("쿼리 실행 실패: " + ex.Message);
                }
            }
        }

        private void chapterBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            verseBox.Items.Clear();

            int bookId = itemBox.SelectedIndex + 1; // 선택한 권의 bookId
            int chapter = Convert.ToInt32(chapterBox.SelectedItem);

            LoadVerses(bookId, chapter); // 선택한 장의 절 수를 로드
        }
        private void LoadVerses(int bookId, int chapter)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Bible WHERE book = @book AND chapter = @chapter";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@book", bookId);
                command.Parameters.AddWithValue("@chapter", chapter);

                try
                {
                    connection.Open();
                    int verseCount = Convert.ToInt32(command.ExecuteScalar()); // 절 수 가져오기
                    verseBox.Items.Clear();// 기존에 있던 절 데이터 삭제
                    verseBox2.Items.Clear();// 기존에 있던 절 데이터 삭제
                    for (int i = 1; i <= verseCount; i++)
                    {
                        verseBox.Items.Add(i); // 절 추가
                        verseBox2.Items.Add(i); // 절 추가
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("쿼리 실행 실패: " + ex.Message);
                }
            }
        }

        private void btnShowInPPT_Click(object sender, EventArgs e)
        {
            if (resultDataGridView.Rows.Count > 0)
            {

                List<string> verses = new List<string>();

                // DataGridView의 모든 행을 가져와서 리스트에 추가.
                foreach (DataGridViewRow row in resultDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null) // "btext" 컬럼에서 값 가져오기
                    {
                        verses.Add(row.Cells[0].Value.ToString());
                    }
                }
                // 가져온 모든 구절을 하나의 문자열로 합치기
                string fullText = string.Join("\n\n", verses);
                string reference = "";

                if (referenceVerse2 == "")
                {
                    reference = referenceBookId + " " + referenceChapter + ":" + referenceVerse;
                }
                else
                {
                    reference = referenceBookId + " " + referenceChapter + ":" + referenceVerse + "~"+referenceVerse2;
                }
                // PowerPoint 실행
                ShowInPowerPoint(fullText, reference);
            }
            else
            {
                MessageBox.Show("DataGridView에 출력된 구절이 없습니다.");
            }

        }
        private string SplitTextIntoLines(string text, int maxLineLength)
        {
            StringBuilder formattedText = new StringBuilder();
            string[] words = text.Split(' '); // 단어 단위로 분할
            int lineLength = 0;

            foreach (string word in words)
            {
                if (lineLength + word.Length > maxLineLength)
                {
                    formattedText.Append("\n"); // 줄바꿈 추가
                    lineLength = 0;
                }

                formattedText.Append(word + " ");
                lineLength += word.Length + 1;
            }

            return formattedText.ToString().Trim();
        }
        private void ShowInPowerPoint(string text, string reference)
        {
            try
            {
                // PowerPoint 애플리케이션 실행
                Type pptType = Type.GetTypeFromProgID("PowerPoint.Application");
                dynamic pptApp = Activator.CreateInstance(pptType);
                pptApp.Visible = true;

                // 새 프레젠테이션 생성
                dynamic presentations = pptApp.Presentations;
                dynamic pptPresentation = presentations.Add();

                // 성경 구절을 줄 단위로 분할
                string[] verses = text.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

                int slideIndex = 1;
                foreach (var verse in verses)
                {
                    // ✅ 단어 단위로 줄바꿈 적용 (최대 20자)
                    string formattedText = SplitTextIntoLines(verse, 20);
                    string firstHalf = "";
                    string secondHalf = "";

                    // MessageBox.Show(lineCount.ToString());
                    // 해당 절의 문자열 길이가 100자 넘어가는 경우(슬라이드 2개로 표현)
                    if (verse.Length > 70)
                    {
                        int lineCount = formattedText.Split('\n').Length;
                        List<string> lines = new List<string>(formattedText.Split('\n'));
                        for (int i=0;i<lineCount;i++)
                        {
                            lines[i] += "\n";
                        }

                        if (lineCount%2 == 0)
                        {
                            for(int i = 0; i < lines.Count; i++)
                            {
                                if (i <lines.Count/2)
                                {
                                    firstHalf += lines[i];
                                }
                                else
                                {
                                    secondHalf += lines[i];
                                }
                                
                            }
                            
                        }
                        else
                        {
                            for (int i = 0; i < lines.Count; i++)
                            {
                                if (i < (lines.Count / 2)+1)
                                {
                                    firstHalf += lines[i];
                                }
                                else
                                {
                                    secondHalf += lines[i];
                                }

                            }
                        }
                        

                        // 슬라이드 추가
                        dynamic slide = pptPresentation.Slides.Add(slideIndex, 1);
                        slide.FollowMasterBackground = 0;
                        slide.Background.Fill.BackColor.RGB = Color.Black.ToArgb();

                        // 슬라이드 추가2
                        dynamic slide2 = pptPresentation.Slides.Add(slideIndex+1, 1);
                        slide2.FollowMasterBackground = 0;
                        slide2.Background.Fill.BackColor.RGB = Color.Black.ToArgb();


                        // ✅ 말씀 위치 추가 (왼쪽 위 구석)
                        dynamic refShape = slide.Shapes.AddTextbox(1, 20, 20, 300, 50);
                        refShape.TextFrame.TextRange.Text = reference;
                        refShape.TextFrame.TextRange.Font.Bold = -1;
                        refShape.TextFrame.TextRange.Font.Size = 24;
                        refShape.TextFrame.TextRange.Font.Color.RGB = ColorTranslator.FromHtml("#FFFFFF80").ToArgb();

                        // ✅ 말씀 위치 추가 (왼쪽 위 구석)2
                        dynamic refShape2 = slide2.Shapes.AddTextbox(1, 20, 20, 300, 50);
                        refShape2.TextFrame.TextRange.Text = reference;
                        refShape2.TextFrame.TextRange.Font.Bold = -1;
                        refShape2.TextFrame.TextRange.Font.Size = 24;
                        refShape2.TextFrame.TextRange.Font.Color.RGB = ColorTranslator.FromHtml("#FFFFFF80").ToArgb();


                        // 텍스트 박스 추가
                        dynamic shape = slide.Shapes.AddTextbox(1, 100, 100, 800, 300);
                        shape.TextFrame2.WordWrap = 0; // ✅ 자동 줄바꿈 해제
                        shape.TextFrame2.VerticalAnchor = 3; // 수직 중앙 정렬

                        // 텍스트 박스 추가2
                        dynamic shape2 = slide2.Shapes.AddTextbox(1, 100, 100, 800, 300);
                        shape2.TextFrame2.WordWrap = 0; // ✅ 자동 줄바꿈 해제
                        shape2.TextFrame2.VerticalAnchor = 3; // 수직 중앙 정렬

                        // 텍스트 설정
                        dynamic textRange = shape.TextFrame.TextRange;
                        textRange.Text = firstHalf;
                        textRange.Font.Size = 45;
                        textRange.Font.Bold = -1;
                        textRange.Font.Color.RGB = Color.White.ToArgb();

                        // 텍스트 설정2
                        dynamic textRange2 = shape2.TextFrame.TextRange;
                        textRange2.Text = secondHalf;
                        textRange2.Font.Size = 45;
                        textRange2.Font.Bold = -1;
                        textRange2.Font.Color.RGB = Color.White.ToArgb();

                        // ✅ 줄 간격 조정
                        dynamic paragraphFormat = shape.TextFrame2.TextRange.ParagraphFormat;
                        paragraphFormat.LineRuleWithin = 2;
                        paragraphFormat.SpaceWithin = 1.5f;
                        textRange.ParagraphFormat.Alignment = 2;

                        // ✅ 줄 간격 조정2
                        dynamic paragraphFormat2 = shape2.TextFrame2.TextRange.ParagraphFormat;
                        paragraphFormat2.LineRuleWithin = 2;
                        paragraphFormat2.SpaceWithin = 1.5f;
                        textRange2.ParagraphFormat.Alignment = 2;

                        // 중앙 정렬
                        float slideWidth = slide.Master.Width;
                        float slideHeight = slide.Master.Height;
                        shape.Left = (slideWidth - shape.Width) / 2;
                        shape.Top = (slideHeight - shape.Height) / 2;

                        // 중앙 정렬2
                        float slideWidth2 = slide2.Master.Width;
                        float slideHeight2 = slide2.Master.Height;
                        shape2.Left = (slideWidth2 - shape2.Width) / 2;
                        shape2.Top = (slideHeight2 - shape2.Height) / 2;

                        slideIndex++;
                        slideIndex++;


                    }
                    // 100자 이하인 경우(슬라이드 하나로 표현)
                    else
                    {

                        // 슬라이드 추가
                        dynamic slide = pptPresentation.Slides.Add(slideIndex, 1);
                        slide.FollowMasterBackground = 0;
                        slide.Background.Fill.BackColor.RGB = Color.Black.ToArgb();


                        // ✅ 말씀 위치 추가 (왼쪽 위 구석)
                        dynamic refShape = slide.Shapes.AddTextbox(1, 20, 20, 300, 50);
                        refShape.TextFrame.TextRange.Text = reference;
                        refShape.TextFrame.TextRange.Font.Bold = -1;
                        refShape.TextFrame.TextRange.Font.Size = 24;
                        refShape.TextFrame.TextRange.Font.Color.RGB = ColorTranslator.FromHtml("#FFFFFF80").ToArgb();


                        // 텍스트 박스 추가
                        dynamic shape = slide.Shapes.AddTextbox(1, 100, 100, 800, 300);
                        shape.TextFrame2.WordWrap = 0; // ✅ 자동 줄바꿈 해제
                        shape.TextFrame2.VerticalAnchor = 3; // 수직 중앙 정렬

                        // 텍스트 설정
                        dynamic textRange = shape.TextFrame.TextRange;
                        textRange.Text = formattedText;
                        textRange.Font.Size = 45;
                        textRange.Font.Bold = -1;
                        textRange.Font.Color.RGB = Color.White.ToArgb();

                        // ✅ 줄 간격 조정
                        dynamic paragraphFormat = shape.TextFrame2.TextRange.ParagraphFormat;
                        paragraphFormat.LineRuleWithin = 2;
                        paragraphFormat.SpaceWithin = 1.5f;
                        textRange.ParagraphFormat.Alignment = 2;

                        // 중앙 정렬
                        float slideWidth = slide.Master.Width;
                        float slideHeight = slide.Master.Height;
                        shape.Left = (slideWidth - shape.Width) / 2;
                        shape.Top = (slideHeight - shape.Height) / 2;

                        slideIndex++;
                    }
                    
                }

                pptPresentation.SlideShowSettings.Run();
            }
            catch (COMException ex)
            {
                MessageBox.Show("PowerPoint 실행 중 오류 발생: " + ex.Message);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // 엔터 키 누르면
            if (e.KeyCode == Keys.Enter)
            {
                string data = fastSearchTextBox.Text.Trim(); // 공백 제거

                // 입력값이 비어있으면 처리하지 않음
                if (string.IsNullOrEmpty(data))
                {

                    return;
                }

                // 텍스트 전처리: 불필요한 문자 제거
                data = data.Replace(": ", " ");
                data = data.Replace(":", " ");
                data = data.Replace("~", " ");
                string[] datas = data.Split(' ');

                // bookId 인덱스 번호 찾기
                int bookId = 0;
                for (int i = 0; i < originalItems.Count; i++)
                {
                    if (originalItems[i] == datas[0])
                    {
                        bookId = i + 1; // 1-based indexing
                        break;
                    }
                }

                // 책 번호가 없으면 경고 메시지 출력
                if (bookId == 0)
                {
                    MessageBox.Show("책을 찾을 수 없습니다.");
                    return;
                }

                // 텍스트박스 초기화 (기존 값 지우기)
                chapterBox.Text = "";
                verseBox.Text = "";
                verseBox2.Text = "";

                // 입력 데이터의 길이에 따라 처리
                if (datas.Length == 3)
                {
                    chapterBox.Text = datas[1];
                    verseBox.Text = datas[2];
                    verseBox2.Text = datas[2]; // 하나의 절만 처리

                    // reference용 저장
                    referenceBookId = datas[0];
                    referenceChapter = datas[1];
                    referenceVerse = datas[2];
                    referenceVerse2 = "";

                }
                else if (datas.Length == 4)
                {
                    chapterBox.Text = datas[1];
                    verseBox.Text = datas[2];
                    verseBox2.Text = datas[3]; // 여러 절 처리

                    // reference용 저장
                    referenceBookId = datas[0];
                    referenceChapter = datas[1];
                    referenceVerse = datas[2];
                    referenceVerse2 = datas[3];
                }

                // 숫자로 변환할 때 예외 처리를 위해 TryParse 사용
                if (int.TryParse(chapterBox.Text, out int chapter) &&
                    int.TryParse(verseBox.Text, out int verse) &&
                    int.TryParse(verseBox2.Text, out int verse2))
                {
                    // 구절 가져오기
                    FetchData(bookId, chapter, verse, verse2);
                }
                else
                {
                    MessageBox.Show("입력된 값이 올바르지 않습니다. 숫자를 확인해 주세요.");
                }

                // 텍스트박스 값 초기화 후 다시 입력을 받을 수 있도록 포커스 설정
                fastSearchTextBox.Clear(); // 검색 후 텍스트박스 초기화
                fastSearchTextBox.Focus(); // 검색 텍스트박스에 포커스
            }
        }
    }
}