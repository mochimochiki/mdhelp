---
title: HTML Helpの構成ファイル
weight: 11
---

HTML Help をビルドするには以下のような構成ファイルが必要です。

|ファイル|説明|
|:--|:--|
| project.hhp |ヘルプのプロジェクトファイル。オプションなどを定義。|
| toc.hhc     |目次定義ファイル|
| Map.h       |HelpID定義ファイル。|

mdhelpはこれらの構成ファイルを基本的に自動で生成します。ただし、プログラムからヘルプIDを指定して特定ページを呼び出すためにヘルプのAPI情報を定義する場合は、自前で`Map.h`を準備する必要があります。mdhelpは`/static/Map.h`を使用してヘルプをコンパイルするため、`/static/Map.h`にマップファイルを作成します。

## Map.h を作成する

mdhelpの`build.bat`でヘルプをビルドすると、`publishDir`に成果物のヘルプファイルと共に`MapTemplate.h`が生成されます。これはヘルプのすべてのページのHelpIDのテンプレートを含みます。プログラムからヘルプのページを呼び出すために`Map.h`を用意する場合、このテンプレートの各IDに対して一意の数値をふり、`/static/Map.h`として保存してから、再ビルドしてください。

{{% note %}}
この作業はページが増えた際にも必要になります。増えたページのID分を`/static/Map.h`に足します。
{{% /note %}}

## project.hhp をカスタマイズする

`project.hhp`はHUGOのテンプレートの仕組みで生成しています。そのため通常のテンプレートと同様`/themes/mdhelp/layouts/index.hhp`を`/layouts/index.hhp`にコピーして編集すれば自由にカスタマイズすることができます。