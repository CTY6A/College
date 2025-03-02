object FShifr: TFShifr
  Left = 118
  Top = 161
  Width = 262
  Height = 260
  BorderIcons = [biSystemMenu]
  Caption = #1064#1080#1092#1088#1086#1074#1072#1085#1080#1077
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 34
    Top = 16
    Width = 155
    Height = 13
    Caption = #1042#1099#1073#1077#1088#1080#1090#1077' '#1084#1077#1090#1086#1076' '#1096#1080#1092#1088#1086#1074#1072#1085#1080#1103':'
  end
  object Label2: TLabel
    Left = 34
    Top = 72
    Width = 198
    Height = 13
    Caption = #1042#1074#1077#1076#1080#1090#1077' '#1082#1086#1076' '#1080#1079' 1 '#1085#1072#1090#1091#1088#1072#1083#1100#1085#1086#1081' '#1094#1080#1092#1088#1099':'
  end
  object Label3: TLabel
    Left = 34
    Top = 128
    Width = 171
    Height = 13
    Caption = #1042#1099#1073#1077#1088#1080#1090#1077' '#1076#1072#1083#1100#1085#1077#1081#1096#1080#1077' '#1076#1077#1081#1089#1090#1074#1080#1103':'
  end
  object ComboBox1: TComboBox
    Left = 34
    Top = 35
    Width = 185
    Height = 21
    ItemHeight = 13
    TabOrder = 0
    Text = #1046#1077#1083#1077#1079#1085#1086#1076#1086#1088#1086#1078#1085#1072#1103' '#1080#1079#1075#1086#1088#1086#1076#1100
    OnChange = ComboBox1Change
    Items.Strings = (
      #1046#1077#1083#1077#1079#1085#1086#1076#1086#1088#1086#1078#1085#1072#1103' '#1080#1079#1075#1086#1088#1086#1076#1100
      #1055#1086#1074#1086#1088#1072#1095#1080#1074#1072#1102#1097#1072#1103#1089#1103' '#1088#1077#1096#1077#1090#1082#1072
      #1052#1077#1090#1086#1076' '#1096#1080#1092#1088#1086#1074#1072#1085#1080#1103' '#1042#1080#1078#1077#1085#1077#1088#1072)
  end
  object Edit1: TEdit
    Left = 34
    Top = 91
    Width = 185
    Height = 21
    MaxLength = 1
    TabOrder = 1
    OnKeyPress = Edit1KeyPress
  end
  object ComboBox2: TComboBox
    Left = 34
    Top = 147
    Width = 185
    Height = 21
    ItemHeight = 13
    TabOrder = 2
    Text = #1047#1072#1096#1080#1092#1088#1086#1074#1072#1090#1100
    Items.Strings = (
      #1047#1072#1096#1080#1092#1088#1086#1074#1072#1090#1100
      #1056#1072#1089#1096#1080#1092#1088#1086#1074#1072#1090#1100)
  end
  object Button2: TButton
    Left = 72
    Top = 175
    Width = 97
    Height = 46
    Caption = #1044#1072#1083#1077#1077
    TabOrder = 3
    OnClick = Button2Click
  end
  object OpenDialog1: TOpenDialog
    Filter = #1090#1077#1082#1089#1090#1086#1074#1099#1081' '#1092#1072#1081#1083'|*.txt'
    Left = 8
    Top = 184
  end
end
