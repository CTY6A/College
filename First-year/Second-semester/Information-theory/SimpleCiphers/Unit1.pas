unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls;

type
  TFShifr = class(TForm)
    Label1: TLabel;
    ComboBox1: TComboBox;
    Label2: TLabel;
    Edit1: TEdit;
    Label3: TLabel;
    ComboBox2: TComboBox;
    OpenDialog1: TOpenDialog;
    Button2: TButton;
    procedure Button2Click(Sender: TObject);
    procedure ComboBox1Change(Sender: TObject);
    procedure Edit1KeyPress(Sender: TObject; var Key: Char);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FShifr: TFShifr;

implementation

{$R *.dfm}

Var
  Encryption:Boolean;
  Key,Ready,s:String;
  FileA: TextFile;

function Fence(Key:String; Fl:boolean): String;
var
  El:string[1];
  SifroS:Array of string;
  posit,i,len:integer;
  zn:boolean;
begin
  setlength(sifros,StrToInt(key));
  i:=1;

  if Key > '1' then
    if Fl then
    begin
      while i<= length(Ready) do
      if not (Ready[i] in ['A'..'Z','a'..'z']) then
        delete(Ready, i, 1)
      else
        inc(i);

      Ready := UpperCase(Ready);

      posit := 0;
      zn := true;
      for i := 1 to length(ready) do
      begin
        SifroS[Posit] := Concat(SifroS[Posit], Ready[i]);
        if  Zn then
          inc(posit)
        else
          dec(posit);

        if (posit=StrToInt(key)-1) or (posit=0) then
          Zn:= not Zn
      end;

      for i := 0 to StrToInt(Key) - 1 do
        Result:=Concat(Result,SifroS[i],' ')
    end
    else
    begin
      while i<= length(Ready) do
      if not (Ready[i] in ['A'..'Z', ' ']) then
        delete(Ready, i, 1)
      else
        inc(i);

      for i := 0 to StrToInt(Key) - 1 do
      begin
        SifroS[i]:=Copy(Ready, 1, Pos(' ', Ready) - 1);
        Delete(Ready, 1, Pos(' ', Ready))
      end;

      posit:=0;
      zn := true;

      while Length(SifroS[Posit]) <> 0 do
      begin
        Result := Concat(Result, SifroS[Posit, 1]);
        Delete(SifroS[posit],1,1);

        if  Zn then
          inc(posit)
        else
          dec(posit);

        if (posit=StrToInt(key)-1) or (posit=0) then
          Zn:= Not(Zn)
      end
    end
  else
    Result := Ready
end;

function Vigener(Key:String; Fl:boolean): String;
const
  Alph=['А'..'Я','Ё'];

var
  i,k,removal,j:integer;
  full:boolean;

begin
  i:=1;
  Ready:=AnsiUpperCase(Ready);
  Key:=AnsiUpperCase(key);
  While i<= length(Ready) do
  begin
    if not (Ready[i] in Alph) then
    begin
      delete(Ready, i, 1);
      dec(i);
    end;
    inc(i);
  end;

  for i := 1 to length(key) do
    if ord(key[i]) >= 198 then
      inc(key[i])
    else
      if ord(key[i]) = 168 then
        key[i] := 'Ж';

  k:=1;
  for i := 1 to length(Ready) do
  begin
    j:=k mod length(key);
    if j=0 then
      j:=length(key);

    if ord(Ready[i]) >= 198 then
      inc(Ready[i])
    else
      if ord(Ready[i]) = 168 then
        Ready[i] := 'Ж';

    removal:=ord(ready[i]);
    if (key[j]<>'А') then
      if Fl then
      begin
        removal :=ord (ready[i]) + ord(key[j])-192;

        if removal>224 then
          removal:=removal-33
      end
      else
      begin
        removal :=ord (Ready[i]) - ord(key[j]) + 192;

        if removal<192 then
          removal:=removal+33
      end;

    key[j]:=char(ord(key[j])+1);

    if (ord(key[j])) = 225 then
      key[j] := 'А';

    if removal > 198 then
      dec(removal)
    else
      if removal = 198 then
        removal := 168;

    ready[i]:=char(removal);
    inc(k)
  end;

  Result:=ready
end;

function Lattice(Fl:boolean): string;
var
  i,Dop,ind:integer;
  str:String;
  ch:Char;
begin
  Ready:=AnsiUpperCase(ready);

  i:=1;
  while i<= length(Ready) do
    if not (Ready[i] in ['A'..'Z']) then
      delete(Ready, i, 1)
    else
      inc(i);

  ch := 'A';
  if length(ready) mod 16 <> 0 then
    for i := length(ready) mod 16 to 15 do
    begin
      ready := ready + Ch;
      inc(ch)
    end;


  if Fl then
    for i := 1 to length(ready) div 16 do
    begin
      Result:=Concat(Result, Ready[1],Ready[13],Ready[9],Ready[5],Ready[6],Ready[10],
                  Ready[14],Ready[2],Ready[11],Ready[7],Ready[3],Ready[15],
                  Ready[16],Ready[4],Ready[8],Ready[12]);
      delete(Ready,1,16)
    end
  else
    for i := 1 to length(ready) div 16 do
    begin
      Result:=Concat(Result, Ready[1],Ready[8],Ready[11],Ready[14],Ready[4],Ready[5],
                  Ready[10],Ready[15],Ready[3],Ready[6],Ready[9],Ready[16],
                  Ready[2],Ready[7],Ready[12],Ready[13]);
      delete(Ready,1,16)
    end
end;

procedure TFShifr.Button2Click(Sender: TObject);
begin
  if (Edit1.Text='') and (ComboBox1.Text <> 'Поворачивающаяся решетка') then
    ShowMessage('Введите код!')
  else
    if OpenDialog1.Execute then
    begin
      AssignFile(FileA, OpenDialog1.FileName);
      Reset(FileA);

      Ready:='';

      while not Eof(FileA) do
      begin
        Readln(FileA,s);
        Ready:=Concat(Ready,s);
      end;
      Key:=Edit1.Text;

      if ComboBox2.Text='Зашифровать' then
        Encryption:=True
      else
        Encryption:=False;

      if (ComboBox1.Text = 'Железнодорожная изгородь') then
        Ready:=Fence(Key,Encryption)
      else
        if (ComboBox1.Text = 'Поворачивающаяся решетка') then
          Ready:=Lattice(Encryption)
        else
          if (ComboBox1.Text = 'Метод шифрования Виженера') then
             Ready:=Vigener(Key,Encryption);

      Rewrite(FileA);
      Write(FileA,Ready);
      CloseFile(FileA);

      ShowMessage('Зашифровка/Расшифровка закончена.'+#10#13+
                 'Текст сохранен в выбранный Вами файл')
    end
end;

procedure TFShifr.ComboBox1Change(Sender: TObject);
begin
   if (ComboBox1.Text = 'Поворачивающаяся решетка') then
   begin
     Label2.Caption:='Для этого метода ключ не нужен';
     Edit1.Enabled:=False;
     Edit1.Color:=cl3DLight;
     Edit1.Text := ''
   end
   else
     if (ComboBox1.Text = 'Железнодорожная изгородь') then
     begin
       Label2.Caption:='Введите код из 1 натуральной цифры:';
       Edit1.Enabled:=True;
       Edit1.Color:=clWindow;
       Edit1.Text := '';
       Edit1.MaxLength := 1
     end
     else
       if (ComboBox1.Text = 'Метод шифрования Виженера') then
       begin
         Label2.Caption:='Введите код из 1 русского слова:';
         Edit1.Enabled:=True;
         Edit1.Color:=clWindow;
         Edit1.Text := '';
         Edit1.MaxLength := 0
       end
end;

procedure TFShifr.Edit1KeyPress(Sender: TObject; var Key: Char);
begin
  if ((ComboBox1.Text = 'Железнодорожная изгородь') and not (Key in ['1'..'9', #8]))
  or ((ComboBox1.Text = 'Метод шифрования Виженера') and not (Key in ['А'..'Я', 'Ё','а'..'я', 'ё', #8])) then
    Key:=#0
end;

end.
