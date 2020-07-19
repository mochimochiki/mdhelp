---
title: content1_page1
weight: 20
---
## Test

このページはテストです。

[リンク](../content2/test.html)

{{% ShowIf supportFuncA %}}
この部分は `config.toml`の`showIfs`に`supportFuncA`を含む構成でのみ表示されます。

* Markdown記述可能。

原稿では以下のように記述します。

```md
{{%/* ShowIf xxx */%}}
ここにxxxをサポートする場合に表示するコンテンツを記述。
{{%/* /ShowIf */%}}
```
{{% /ShowIf %}}