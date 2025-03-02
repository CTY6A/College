unit UTI3LABA;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls;

type
  TFMain = class(TForm)
    EP: TEdit;
    EQ: TEdit;
    EB: TEdit;
    MSource: TMemo;
    MResult: TMemo;
    DOMain: TOpenDialog;
    DSMain: TSaveDialog;
    BOF: TButton;
    BSF: TButton;
    BStart: TButton;
    L3: TLabel;
    L31: TLabel;
    L21: TLabel;
    RBC: TRadioButton;
    RBDC: TRadioButton;
    procedure EPKeyPress(Sender: TObject; var Key: Char);
    procedure BOFClick(Sender: TObject);
    procedure BSFClick(Sender: TObject);
    procedure BStartClick(Sender: TObject);
  private

  public
    { Public declarations }
  end;

var
  FMain: TFMain;

  F1, F2, F3, F4, F5: file;

  Byte1: Byte;

  i, q, p, b, c, x1, y1, D, mp, mq, d1, d2, d3, d4: Integer;

  ResultKey, x2, x3: string;

implementation

{$R *.dfm}

function Check(a: Integer): Boolean;
var
  i: Integer;
begin
  Result := True;
  i := 2;
  while (i < a) and Result do
    if a mod i = 0 then
      Result := False
    else
      Inc(i)
end;

function EuclidEx(a,b: Integer): Integer;
var
  d0, d2, x0, x2, y0, y2, q: Integer;
begin
  d0:=a;
  Result:=b;
  x0:=1;
  x1:=0;
  y0:=0;
  y1:=1;
  while Result>1 do
  begin
    q:=d0 div Result;
    d2:=d0 mod Result;
    x2 := x0 - q * x1;
    y2 := y0 - q * y1;
    d0:=Result;
    Result:=d2;
    x0:=x1;
    x1:=x2;
    y0:=y1;
    y1:=y2
  end
end;

function fast_exp(a,z,n: Integer): Integer;
var
  a1, z1: Integer;
begin
  a1:=a;
  z1:=z;
  Result:=1;
  while z1<>0 do
  begin
    while (z1 mod 2)=0 do
    begin
      z1:=z1 div 2;
      a1:=(a1*a1) mod n
    end;
    z1:=z1-1;
    Result:=(Result*a1) mod n
  end
end;

procedure TFMain.EPKeyPress(Sender: TObject; var Key: Char);
begin
  if not (Key in ['0'..'9',#8]) then
    Key:=#0
end;

procedure TFMain.BOFClick(Sender: TObject);
begin
  if (EP.Text <> '') or (EQ.Text <> '') or (EB.Text <> '') then
  begin
    p := StrToInt(EP.Text);
    q := StrToInt(EQ.Text);
    b := StrToInt(EB.Text);

    if Check(p) and (p > 1) and (p mod 4 = 3) and (q mod 4 = 3) and Check(q) and (q > 1) and (b < p * q) and (p <> q) and (p*q > 255) and (p*q < 65535) then
    begin
      if DOMain.Execute then
      begin
        MSource.Text := '';

        AssignFile(F1,DOMain.FileName);
        if RBC.Checked then
          Reset(F1,1)
        else
          Reset(F1,2);

        i := 0;

        while (not Eof(F1)) and (i<=1000) do
        begin
          BlockRead(F1,c,1);
          MSource.Text:=MSource.Text +inttostr(c) + ' ';
          inc(i)
        end;

        CloseFile(F1);
        if i = 1001 then
          ShowMessage('Файл оказался слишком большим. Отображено только 1000 байт.');
        BSF.Enabled := True
      end
    end
    else
      ShowMessage('p и q должны быть простыми'+#10+#13
                 +'p и q должны быть равны 3 по модулю 4'+#10+#13
                 +'b должно быть меньше p*q'+#10+#13
                 +'p и q должны быть разными'+#10+#13
                 +'p * q должно быть больше 255 и меньше 65535');
  end
  else
    ShowMessage('Заполните все поля!')
end;

procedure TFMain.BSFClick(Sender: TObject);
begin
  if DSMain.Execute then
  begin
    MResult.Text := '';

    AssignFile(F2,DSMain.FileName);

    BStart.Enabled:= True;
  end;
  if RBDC.Checked then
  begin
    if DSMain.Execute then
      AssignFile(F3,DSMain.FileName);

    if DSMain.Execute then
      AssignFile(F4,DSMain.FileName);

    if DSMain.Execute then
      AssignFile(F5,DSMain.FileName)
  end
end;

procedure TFMain.BStartClick(Sender: TObject);
begin
  if RBC.Checked then
  begin
    Reset(F1,1);
    Rewrite(F2,1);
    while not Eof(F1) do
    begin
      BlockRead(F1,c,1);

      c := ((c) * (c + b)) mod (p * q);

      BlockWrite(F2,c,2)
    end
  end
  else
  begin
    Reset(F1,2);
    Rewrite(F2,1);
    Rewrite(F3,1);
    Rewrite(F4,1);
    Rewrite(F5,1);

    while not Eof(F1) do
    begin
      BlockRead(F1,c,1);
      D := (b*b+4*c) mod (p * q);
      mp := fast_exp(D,(p+1)div 4, p);
      mq := fast_exp(D,(q+1)div 4, q);
      EuclidEx(p,q);
      d1 := Abs(x1*p*mq + y1*q*mp) mod(p*q);
      d2 := p*q - d1;
      d3 := Abs(x1*p*mq - y1*q*mp) mod(p*q);
      d4 := p*q - d3;

      if ((d1 - b) mod 2 = 0)  then
      begin
        c := (d1 - b) div 2 mod (p*q);

        if c < 0 then
          c := c + p*q
      end
      else
        c := (d1 - b + (p*q)) div 2 mod (p*q);
      BlockWrite(F2,c,1);

      if (d2 - b) mod 2 = 0 then
      begin
        c := (d2 - b) div 2 mod (p*q);

        if c < 0 then
          c := c + p*q
      end
      else
        c := (d2 - b + (p*q)) div 2 mod (p*q);
      BlockWrite(F3,c,1);

      if (d3 - b) mod 2 = 0 then
      begin
        c := (d3 - b) div 2 mod (p*q);

        if c < 0 then
          c := c + p*q
      end
      else
        c := (d3 - b + (p*q)) div 2 mod (p*q);
      BlockWrite(F4,c,1);

      if (d4 - b) mod 2 = 0 then
      begin
        c := (d4 - b) div 2 mod (p*q);

        if c < 0 then
          c := c + p*q
      end
      else
        c := (d4 - b + (p*q)) div 2 mod (p*q);
      BlockWrite(F5,c,1);
    end;
    CloseFile(F3);
    CloseFile(F4);
    CloseFile(F5);
  end;

  CloseFile(F1);
  if RBDC.Checked then
    Reset(F2,1)
  else
    Reset(F2,2);

  MResult.Text := '';
  i:=0;
  while (not Eof(F2)) and (i<=1000) do
  begin
    BlockRead(F2,c,1);

    MResult.Text:=MResult.Text + inttostr(c) + ' ';

    Inc(i)
  end;

  CloseFile(F2);

  if i = 1001 then
    ShowMessage('Файл оказался слшком большим. Отображено только 600 байт.');
end;

end.
