---
title: "include"
weight: 30
---

## include md

{{< include includetest.md >}}

## include csv from current dir

{{< include "./current-test.csv" >}}

## include csv from relative path

{{< include "./csv/relative-test.csv" >}}

## include csv from _include dir

{{< include test.csv >}}

## include csv one parameter

### パラメータをinclude

{{< include "test.csv" "005" "名称" >}}

### 列がマッチしない

↓表示されないはず。
{{< include "test.csv" "005" "存在しない列名" >}}

### 行がマッチしない

↓表示されないはず。
{{< include "test.csv" "9999" "名称" >}}

### パラメータが足りない

↓表示されないはず。
{{< include "test.csv" "005" >}}

## include csv - 複雑な表

{{< include
      src="./cell-merge1.csv"
      class="simple"
      markdown=true
      merge=true
      head=true
      width="90%"
      width-ratio="5%-10%-75%-*" >}}

ヘッダでない1行目が空セルの場合は列数が足りなくなる。↓

{{< include
      src="./cell-merge2.csv"
      class="simple"
      markdown=true
      merge=true
      head=false
      width="90%"
      width-ratio="5%-10%-75%-*" >}}

Markdownテスト。ショートコードはレンダリングされない。

{{< include
      src="./cell-merge3.csv"
      class="gray"
      markdown=true
      merge=true
      head=true
      width="90%"
      width-ratio="5%-10%-75%-*" >}}