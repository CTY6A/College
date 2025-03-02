unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls;

type
  TFMain = class(TForm)
    ComboBox1: TComboBox;
    Edit1: TEdit;
    Button1: TButton;
    Button2: TButton;
    Label1: TLabel;
    Memo1: TMemo;
    Memo3: TMemo;
    Label3: TLabel;
    Label4: TLabel;
    Label7: TLabel;
    OpenDialog1: TOpenDialog;
    Button3: TButton;
    SaveDialog1: TSaveDialog;
    Label8: TLabel;
    Label9: TLabel;
    Edit2: TEdit;
    Label2: TLabel;
    Label10: TLabel;
    Edit3: TEdit;
    Edit4: TEdit;
    Memo2: TMemo;
    Label6: TLabel;
    Memo4: TMemo;
    Label11: TLabel;
    Button4: TButton;
    Label12: TLabel;
    Label13: TLabel;
    Label5: TLabel;
    Edit5: TEdit;
    OpenDialog2: TOpenDialog;
    Edit6: TEdit;
    Label14: TLabel;
    procedure ComboBox1Change(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure ComboBox1KeyPress(Sender: TObject; var Key: Char);
    procedure Edit1KeyPress(Sender: TObject; var Key: Char);
    procedure FormShow(Sender: TObject);
  private

  public

  end;

var
  FMain: TFMain;
  FileN1, FileN2, FileN3:string;

implementation
{$R *.dfm}

function fast_exp(a:Cardinal;z,n: cardinal):word;
var
  x: cardinal;
begin
  x:=1;
  while z<>0 do
  begin
    while (z mod 2)=0 do
    begin
      z:=z div 2;
      a:=(a*a) mod n
    end;
    dec(z);
    x:=(x*a) mod n;
  end;
  result:=x;
end;

/////////////////ПРОЦЕДУРА ПОДПИСИ//////////////
procedure Subscribe(p,q,x,k,h1:cardinal);
var
  f, c: file;
  rez:integer;
  buf: byte;
  H: cardinal;
  s,g,r,y: cardinal;

begin
  assign(f,FileN1);
  reset(f,1);

  assign(c,FileN2);
  rewrite(c,1);

  g:=fast_exp(h1,((p-1)div q),p);
  y:=fast_exp(g,x,p);
  FMain.Edit5.Text:=IntToStr(y);

  H:=100;
  while not Eof(f) do
  begin
    blockread(f, buf, 1, rez);
    H:=((H+buf)*(H+buf)) mod q;
    blockwrite(c,buf,1,rez);
  end;
  FMain.Memo2.Text:= IntToStr(H);

  r:=fast_exp(g,k,p) mod q;
  s:=(fast_exp(k,q-2,q)*(H+r*x)) mod q;
  if (r<>0) and (s<>0) then
  begin
    blockwrite(c,#13#10,2,rez);
    blockwrite(c, s, 1, rez);
    blockwrite(c, r, 1, rez);
    FMain.Memo3.Text:='r = '+IntToStr(r)+#13#10+'s = '+IntToStr(s);
    showMessage('Результаты сохранены');
  end
  else
    ShowMessage('Выберите другое k');
  close(c);
  close(f);
end;


////////////ПРОВЕРКА////////////////
procedure Checking(p,q,y,h1:cardinal);
var
  c: file;
  rez:integer;
  buf,s,r: byte;
  size,i: integer;
  w,v,u1,u2,H,g:cardinal;
begin
  assign(c,FileN3);
  reset(c,1);

  H:=100;
  size:=FileSize(c)-4;
  i:=1;
  while (i<=Size) do
   begin
    blockread(c, buf, 1, rez);
    H:=((H+buf)*(H+buf)) mod q;
    inc(i);
  end;
  FMain.Memo2.Text:= IntToStr(H);
  blockRead(c,buf,1,rez);
  blockread(c,buf,1,rez);
  blockread(c, s, 1, rez);
  blockread(c, r, 1, rez);
  FMain.Memo3.Text:='r = '+IntToStr(r)+#13#10+'s = '+IntToStr(s);

  g:=fast_exp(h1,((p-1)div q),p);

  w:=Fast_exp(s,q-2,q);
  u1:=(H*w) mod q;
  u2:=(r*w) mod q;
  v :=((fast_exp(g,u1,p)*fast_exp(y,u2,p))mod p) mod q;

  if (v=r) then
    FMain.Memo4.Text:='Подпись подлинна'+#13#10+'v = r = '+IntToStr(v)
  else
    FMain.Memo4.Text:='Подпись не подлинна'+#13#10+'v<>r'+#13#10+'v = '+IntToStr(v);

  close(c);
end;

procedure TFMain.Button1Click(Sender: TObject);
var
  f:File;
  i,rez: integer;
  bt:char;
begin
  FMain.Memo1.Text:='';
  FileN1:='';
  if OpenDialog1.Execute then
  begin
    FileN1:=OpenDialog1.FileName;
    assignfile(f,FileN1);
    reset(f,1);
    i:=0;
    while (not(EOF(f))) and (i<1000) do
    begin
      BlockRead(F,Bt,1,rez);
      memo1.Text:=Memo1.Text+Bt;
      Inc(i);
    end;
   Button1.Caption:='ФО выбран';
   CloseFile(F);
  end;
end;

function Check_simple(x:integer):boolean;
var
  i: integer;
  fl: boolean;
begin
  fl:=true;
  i:=2;
  while ((i<=sqrt(x))and(fl))do
  begin
    if(x mod i = 0)then
      fl:=false;
    inc(i);
  end;
  if (x<=3) then
    fl:=true;
  result:=fl;
end;


procedure TFMain.Button2Click(Sender: TObject);
var
  Pr, del, int,h_ch: boolean;
  p,q,k,x,h:Cardinal;
begin
  if (Edit1.Text<>'') and (Edit2.Text<>'')
  and (((ComboBox1.Text='Подписать') and (FileN1<>'') and(Edit4.Text<>'') and (FileN2<>'') and (Edit3.Text<>'')) or
  ((ComboBox1.Text='Проверить') and (FileN3<>'') and (Edit5.Text<>''))) then
  begin
    p:=StrToInt(Edit1.Text);
    q:=StrToInt(Edit2.Text);
    h:=StrToInt(Edit6.Text);

    if ((h<(p-1)) and (h>1)) then
      h_ch:=true
    else
    begin
      h_ch:=false;
      showMessage('h должно быть в интервале(1,p-1)')
    end;

    if (fast_exp(h,((p-1)div q),p)<=1) then
    begin
      h_ch:=false;
      showMessage('g получилось равным 1. поменяйте значения');
    end;

    Pr:=Check_Simple(p) and Check_Simple(q);
    if (p=1) then
      Pr:=false;
    if Pr=false then
      ShowMessage('Проверте, чтобы числа p и q были простыми');

    if ((p-1) mod q = 0) then
      del:=true
    else
    begin
      del:=false;
      ShowMessage('Число q должно быть делителем (p-1)');
    end;


////////////ПОДПИСЬ//////////
    if (ComboBox1.Text='Подписать') then
    begin
      k:=StrToInt(Edit3.Text);
      x:=StrToInt(Edit4.Text);
      if (k=0) or (k>=q) or (x=0) or (x>=q) then
      begin
        int:=false;
        ShowMessage('Проверте, чтобы числа k и x принадлежали интервалу (0,q)');
      end
      else
        int:=true;

      if Pr and del and int and h_ch then
        Subscribe(p,q,x,k,h);
    end
/////////ПРОВЕРКА//////////
    else
      if Pr and Del and h_ch then
        Checking(p,q,StrToInt(Edit5.Text),h);
    FMain.Button1.Caption:='Открыть...';
    FMain.Button3.caption:='Сохранить в...';
  end
  else
  begin
    if (ComboBox1.Text='Подписать') and (FileN2='') then
      showMessage('Выберите файл для сохранения!');
    if Memo1.Text='' then
      ShowMessage('Выберите файл для подписи!');
    if (ComboBox1.Text='Проверить') and (FileN3='') then
      ShowMessage('Выберите файл для проверки');
    if (Edit1.Text='') or (Edit2.Text='') or
    ((ComboBox1.Text='Подписать') and ((Edit4.Text='') or (Edit3.Text='')))
    or ((ComboBox1.Text='Проверить') and (Edit5.Text='')) then
      ShowMessage('Недостаточно данных для начала работы');
  end;

end;

procedure TFMain.Button3Click(Sender: TObject);
begin
  FileN2:='';
  if SaveDialog1.Execute then
  begin
    FileN2:=SaveDialog1.FileName;
    Button3.Caption:='ФС выбран';
  end;
end;

procedure TFMain.Button4Click(Sender: TObject);
var
  f:file;
  size,i,j,rez:integer;
  bt: char;
begin
  Memo1.Text:='';
  FileN3:='';
  if OpenDialog2.Execute then
  begin
    FileN3:=OpenDialog2.FileName;
    Button4.Caption:='Выбран';
    assignfile(f,FileN3);
    reset(f,1);
    i:=0;
    j:=0;
    size:=FileSize(f)-4;
    while (i<=size) and (j<1000) do
    begin
      BlockRead(F,Bt,1,rez);
      memo1.Text:=Memo1.Text+Bt;
      Inc(i);
      inc(j);
    end;
   CloseFile(F);
  end;
end;

procedure TFMain.ComboBox1Change(Sender: TObject);
begin
  if ComboBox1.Text='Подписать'then
  begin
    Button3.Enabled:=true;
    Button1.Enabled:=true;
    Memo4.Enabled:=False;
    Button4.Enabled:=false;
    Edit3.Enabled:=True;
    Edit4.Enabled:=true;
    Edit5.Enabled:=false;
  end
  else
  begin
    Button3.Enabled:=false;
    Button1.Enabled:=false;
    Memo4.Enabled:=True;
    Button4.Enabled:=True;
    Edit3.Enabled:=false;
    Edit4.Enabled:=false;
    Edit5.Enabled:=true;
  end;
end;

procedure TFMain.ComboBox1KeyPress(Sender: TObject; var Key: Char);
begin
  key:=#0;
end;

procedure TFMain.Edit1KeyPress(Sender: TObject; var Key: Char);
begin
  if not(key in ['0'..'9',#8]) then
    key:=#0;
end;


procedure TFMain.FormShow(Sender: TObject);
begin
  Memo1.Text:='';
  Memo2.Text:='';
  Memo3.Text:='';
  Memo4.Text:='';
  FileN1:='';
  FileN2:='';
  FileN2:='';
  Memo4.Enabled:=False;
  Button4.Enabled:=false;
  Button3.Enabled:=true;
  Edit5.Enabled:=false;
  Button1.Enabled:=true;
  Edit6.Text:='2';
end;

end.
