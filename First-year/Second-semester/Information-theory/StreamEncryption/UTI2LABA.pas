unit UTI2LABA;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls;

type
  TFMain = class(TForm)
    CBEncryption: TComboBox;
    EFirst: TEdit;
    ESecond: TEdit;
    EThird: TEdit;
    MSource: TMemo;
    MKey: TMemo;
    MResult: TMemo;
    DOMain: TOpenDialog;
    DSMain: TSaveDialog;
    BOF: TButton;
    BGK: TButton;
    BSF: TButton;
    BStart: TButton;
    Lx1: TLabel;
    L1: TLabel;
    L2: TLabel;
    L3: TLabel;
    L31: TLabel;
    L21: TLabel;
    procedure CBEncryptionKeyPress(Sender: TObject; var Key: Char);
    procedure CBEncryptionChange(Sender: TObject);
    procedure EFirstKeyPress(Sender: TObject; var Key: Char);
    procedure BOFClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure BGKClick(Sender: TObject);
    procedure BSFClick(Sender: TObject);
    procedure BStartClick(Sender: TObject);
  private
    function ByteToBin(Byte1: Byte): string;
    function LFSR(Key, P: string): string;
    function BinTobyte(Value: string): byte;
  public
    { Public declarations }
  end;

var
  FMain: TFMain;

  F1, F2: file;

  Byte1: Byte;

  i: Integer;

  ResultKey, x1, x2, x3: string;

implementation

{$R *.dfm}



procedure TFMain.FormActivate(Sender: TObject);
begin
  CBEncryption.Text:='LFSR1';
end;

procedure TFMain.CBEncryptionKeyPress(Sender: TObject; var Key: Char);
begin
  key:=#0;
end;

procedure TFMain.CBEncryptionChange(Sender: TObject);
begin
  EFirst.Text := '';

  if CBEncryption.Text = 'LFSR1' then
  begin
    EFirst.MaxLength := 26;
    L1.Visible:=True
  end
  else
    L1.Visible:=False;

  if CBEncryption.Text = 'LFSR2' then
  begin
    EFirst.MaxLength := 34;
    L2.Visible:=True
  end
  else
    L2.Visible:=False;

  if CBEncryption.Text = 'LFSR3' then
  begin
    EFirst.MaxLength := 24;
    L3.Visible:=True
  end
  else
    L3.Visible:=False;

  if CBEncryption.Text = 'Геффе' then
  begin
    EFirst.MaxLength := 26;
    EFirst.Visible := True;
    ESecond.Visible := True;
    EThird.Visible := True;
    Lx1.Visible:=True;
    L1.Visible:=True;
    L21.Visible:=True;
    L31.Visible:=True;
  end
  else
  begin
    Lx1.Visible:=False;
    L21.Visible:=False;
    L31.Visible:=False;
  end;
end;

procedure TFMain.EFirstKeyPress(Sender: TObject; var Key: Char);
begin
  if not (Key in ['0','1',#8]) then
    Key:=#0
end;

function TFMain.ByteToBin(Byte1: Byte): string;
var
  i: Byte;
begin
  Result:='';
  for i:=8 downto 1 do
  begin
    Result:=IntToStr(Byte1 mod 2) + Result;
    Byte1:= Byte1 div 2;
  end
end;

procedure TFMain.BOFClick(Sender: TObject);
begin
  if DOMain.Execute then
  begin
    MSource.Lines.Clear;

    AssignFile(F1,DOMain.FileName);
    Reset(F1,1);

    i:=0;
    while (not Eof(F1)) and (i<=100) do
    begin
      BlockRead(F1,Byte1,1);
      MSource.Lines[i]:=MSource.Lines[i] + ByteToBin(Byte1);
      if Length(MSource.Lines[i]) = 48 then
      begin
        MSource.Lines.Add('');
        Inc(i);
      end;
    end;

    CloseFile(F1);
    if i = 101 then
      ShowMessage('Файл оказался слшком большим. Отображено только 600 байт.');

    BGK.Enabled := True;
    BSF.Enabled := True;
    MKey.Text:='';
  end;
end;

function TFMain.LFSR (Key, P: string): string;

var
  i,x1,x2,x3,x4: Integer;

begin
  case P[5] of
    '1' : begin
            x1 := 26; x2 := 8; x3 := 7; x4 := 1;
          end;

    '2' : begin
            x1 := 34; x2 := 15; x3 := 14; x4 := 1;
          end;

    '3' : begin
            x1 := 24; x2 := 4; x3 := 3; x4 := 1;
          end
  end;

  Result := Key;
  Reset(F1,1);
  while Length(Result)<FileSize(F1)*8 do
  begin
    Result:=Result+IntToStr(StrToInt(Result[Length(Result)- x1 + 1])
    xor StrToInt(Result[Length(Result)- x2 + 1])
    xor StrToInt(Result[Length(Result)- x3 + 1])
    xor StrToInt(Result[Length(Result) - x4 + 1]));
  end;

  CloseFile(F1);
end;

procedure TFMain.BGKClick(Sender: TObject);
begin
  Reset(F1,1);
  i:=FileSize(F1);
  CloseFile(F1);
  if (CBEncryption.Text = 'LFSR1') or
     (CBEncryption.Text = 'LFSR2') or
     (CBEncryption.Text = 'LFSR3') then
     if Length(EFirst.Text) = EFirst.MaxLength then
     begin
       ResultKey:=LFSR(EFirst.Text,CBEncryption.Text);
       MKey.Text:=Copy(ResultKey,1,i*8);
     end
     else
       ShowMessage('Вы ввели недостаточное количество символов!')
  else
    if (Length(EFirst.Text) = EFirst.MaxLength) and
       (Length(ESecond.Text) = ESecond.MaxLength) and
       (Length(EThird.Text) = EThird.MaxLength) then
    begin
      ResultKey:='';
      x1:=Copy(LFSR(EFirst.Text,'LFSR1'),1,i*8);
      x2:=Copy(LFSR(ESecond.Text,'LFSR2'),1,i*8);
      x3:=Copy(LFSR(EThird.Text,'LFSR3'),1,i*8);

      Reset(F1,1);
      i:=1;

      while i div 8 < FileSize(F1) do
      begin
        ResultKey:=ResultKey + ByteToBin((BinTobyte(Copy(x1,i,8)) and BinTobyte(Copy(x2,i,8))) or (not BinTobyte(Copy(x1,i,8)) and BinTobyte(Copy(x3,i,8))));
        inc(i,8);
      end;

      CloseFile(F1);
      MKey.Text:=ResultKey;
    end
    else
      ShowMessage('Вы ввели недостаточное количество символов!');

end;

procedure TFMain.BSFClick(Sender: TObject);
begin
  if DSMain.Execute then
  begin
    MResult.Lines.Clear;

    AssignFile(F2,DSMain.FileName);

    BStart.Enabled:= True;
  end;
end;

function TFMain.BinTobyte(Value: string): byte;
var
  i: byte;
begin
  Result := 0;
  for i := 8 downto 1 do
    if Value[i] = '1' then Result := Result + (1 shl (8 - i));
end;

procedure TFMain.BStartClick(Sender: TObject);
begin

  Reset(F1,1);
  Rewrite(F2,1);

  i:=1;
  while not Eof(F1) do
  begin
    BlockRead(F1,byte1,1);
    byte1:=BinTobyte(Copy(ResultKey,i,8)) xor byte1;

    BlockWrite(F2,byte1,1);

    i:=i+8;
  end;

  Reset(F2,1);
  MResult.Lines.Clear;
  i:=0;
  while (not Eof(F2)) and (i<=100) do
  begin
    BlockRead(F2,Byte1,1);
    MResult.Lines[i]:=MResult.Lines[i] + ByteToBin(Byte1);
    if Length(MResult.Lines[i]) = 48 then
    begin
      MResult.Lines.Add('');
      Inc(i);
    end;
  end;

  CloseFile(F2);
  CloseFile(F1);
  if i = 101 then
    ShowMessage('Файл оказался слшком большим. Отображено только 600 байт.');
end;

end.
