function PagePrint()
{
    var btnPrint = window.document.getElementById("btnPrint");
    var btnClose = window.document.getElementById("btnClose");
    try
    {
        btnPrint.style.display = "none";
        btnClose.style.display = "none";
//        document.all.factory.printing.SetMarginMeasure(2)    // measure margins in inches  
//        document.all.factory.SetPageRange(false, 1, 3)          // need pages from 1 to 3  
//        document.all.factory.printing.printer = "HP DeskJet 870C"  
//        document.all.factory.printing.copies = 2  
//        document.all.factory.printing.collate = true  
//        document.all.factory.printing.paperSize = "A4"  
//        document.all.factory.printing.paperSource = "Manual feed"  

        document.all.factory.printing.header = "";
        document.all.factory.printing.footer = "";
        document.all.factory.printing.portrait = true; //◊›œÚ
//            
//        document.all.factory.printing.leftMargin =  10;   //◊Û“≥±ﬂæ‡
//        document.all.factory.printing.topMargin = 10;     //…œ“≥±ﬂæ‡
//        document.all.factory.printing.rightMargin = 10;  //”““≥±ﬂæ‡
//        document.all.factory.printing.bottomMargin = 10; //œ¬“≥±ﬂæ‡            
    //      document.all.factory.printing.Preview();
    //      document.all.factory.PageSetup();
//        window.print();
        document.all.factory.printing.Print(true);
        btnPrint.style.display = "";
        btnClose.style.display = "";
   }
   catch(e)
   {
    
   }

}


               

