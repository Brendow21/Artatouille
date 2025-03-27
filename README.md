<!DOCTYPE html>
<html lang="en">
  <head>
  
  <script defer data-domain="markdownguide.org" src="https://hawk.forklabs.com/js/plausible.js"></script>
  

  <meta charset="utf-8">
  <link rel="shortcut icon" type="image/x-icon" href="/favicon.ico"/>
  <link rel="apple-touch-icon" sizes="180x180" href="/assets/favicons/apple-touch-icon.png">
  <link rel="mask-icon" href="/assets/favicons/safari-pinned-tab.svg" color="#5bbad5">
  <meta name="viewport" content="width=device-width, initial-scale=1">

  <title>Markdown Cheat Sheet | Markdown Guide</title>

  <meta name="description" content="A quick reference to the Markdown syntax.">
  <meta name="twitter:card" content="summary" />
  <meta property="og:type" content="website" />
  <meta property="og:url" content="https://www.markdownguide.org/cheat-sheet/" />
  <meta property="og:title" content="Markdown Cheat Sheet | Markdown Guide" />
  <meta property="og:description" content="A quick reference to the Markdown syntax." />
  <meta property="og:image" content="https://www.markdownguide.org/assets/images/markdown-guide-og.jpg" />

  <meta name="HandheldFriendly" content="True">
  <meta name="MobileOptimized" content="320">

  
  
  <link href="/assets/css/pagebundle.css" rel="stylesheet">
  
  
  
  <link rel="canonical" href="https://www.markdownguide.org/cheat-sheet/">

  <script src="/assets/javascript/jquery-3.5.1.slim.min.js"></script>
  
  <script async src="/assets/javascript/bs.bundle.min.js"></script>
  <script defer src="/assets/javascript/fa.bundle.min.js"></script>
  
  <script async src="https://media.ethicalads.io/media/client/ethicalads.min.js"></script>
</head>

  <body data-spy="scroll" data-target="#toc">
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark mdg-nav">
  <div class="container">
    <a class="navbar-brand" href="/" style="margin-top: 2px; margin-bottom: 3px">
      <img src="/assets/images/markdown-mark-white.svg" height="23" width="47" class="d-inline-block align-top" alt="Markdown Guide" style="margin-top: 4px; padding-right: 10px">
      Markdown Guide
    </a>
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarNavAltMarkup" style="margin-top: 2px">
      <div class="navbar-nav">
        <a class="nav-item nav-link " href="/getting-started/">Get Started</a>
        <a class="nav-item nav-link active" href="/cheat-sheet/">Cheat Sheet</a>
        <a class="nav-item nav-link " href="/basic-syntax/">Basic Syntax</a>
        <a class="nav-item nav-link " href="/extended-syntax/">Extended Syntax</a>
        <a class="nav-item nav-link " href="/hacks/">Hacks</a>
        <a class="nav-item nav-link " href="/tools/">Tools</a>
        <a class="nav-item nav-link " href="/book/">Book</a>
      </div>
    </div>
    <form class="form-inline d-none d-lg-inline-block mt-2 mt-md-0">
      <input type="text" class="form-control" id="search-input" placeholder="Search" style="margin-top:0">
    </form>
  </div>
</nav>

    <!-- Main jumbotron for a primary marketing message or call to action -->
    <div class="jumbotron">
      <div class="container">
        <h1 class="no-anchor" data-toc-skip>Markdown Cheat Sheet</h1>
        <p>A quick reference to the Markdown syntax.</p>
      </div>
    </div>
    <div class="container">
      <div class="row row-offcanvas row-offcanvas-right">
        <div class="col-xs-12 col-sm-12 col-md-9">
          <h2 id="overview">Overview</h2>

<p>This Markdown cheat sheet provides a quick overview of all the Markdown syntax elements. It can’t cover every edge case, so if you need more information about any of these elements, refer to the reference guides for <a href="/basic-syntax/">basic syntax</a> and <a href="/extended-syntax/">extended syntax</a>.</p>

<h2 id="basic-syntax">Basic Syntax</h2>

<p>These are the elements outlined in John Gruber’s original design document. All Markdown applications support these elements.</p>

<table class="table table-bordered">
  <thead class="thead-light">
    <tr>
      <th>Element</th>
      <th>Markdown Syntax</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td><a href="/basic-syntax/#headings">Heading</a></td>
      <td><code># H1<br />
          ## H2<br />
          ### H3</code></td>
    </tr>
    <tr>
      <td><a href="/basic-syntax/#bold">Bold</a></td>
      <td><code>**bold text**</code></td>
    </tr>
    <tr>
      <td><a href="/basic-syntax/#italic">Italic</a></td>
      <td><code>*italicized text*</code></td>
    </tr>
    <tr>
      <td><a href="/basic-syntax/#blockquotes-1">Blockquote</a></td>
      <td><code>&gt; blockquote</code></td>
    </tr>
    <tr>
      <td><a href="/basic-syntax/#ordered-lists">Ordered List</a></td>
      <td><code>
        1. First item<br />
        2. Second item<br />
        3. Third item<br />
      </code></td>
    </tr>
    <tr>
      <td><a href="/basic-syntax/#unordered-lists">Unordered List</a></td>
      <td>
        <code>
          - First item<br />
          - Second item<br />
          - Third item<br />
        </code>
      </td>
    </tr>
    <tr>
      <td><a href="/basic-syntax/#code">Code</a></td>
      <td><code>`code`</code></td>
    </tr>
    <tr>
      <td><a href="/basic-syntax/#horizontal-rules">Horizontal Rule</a></td>
      <td><code>---</code></td>
    </tr>
    <tr>
      <td><a href="/basic-syntax/#links">Link</a></td>
      <td><code>[title](https://www.example.com)</code></td>
    </tr>
    <tr>
      <td><a href="/basic-syntax/#images-1">Image</a></td>
      <td><code>![alt text](image.jpg)</code></td>
    </tr>
  </tbody>
</table>

<h2 id="extended-syntax">Extended Syntax</h2>

<p>These elements extend the basic syntax by adding additional features. Not all Markdown applications support these elements.</p>

<table class="table table-bordered">
  <thead class="thead-light">
    <tr>
      <th>Element</th>
      <th>Markdown Syntax</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td><a href="/extended-syntax/#tables">Table</a></td>
      <td><code>
          | Syntax      | Description |<br />
          | ----------- | ----------- |<br />
          | Header      | Title       |<br />
          | Paragraph   | Text        |
      </code></td>
    </tr>
    <tr>
      <td><a href="/extended-syntax/#fenced-code-blocks">Fenced Code Block</a></td>
      <td><code>```<br />
      {<br />
      &nbsp;&nbsp;"firstName": "John",<br />
      &nbsp;&nbsp;"lastName": "Smith",<br />
      &nbsp;&nbsp;"age": 25<br />
      }<br />
      ```
      </code></td>
    </tr>
    <tr>
      <td><a href="/extended-syntax/#footnotes">Footnote</a></td>
      <td><code>
        Here's a sentence with a footnote. [^1]<br /><br />
        [^1]: This is the footnote.
      </code></td>
    </tr>
    <tr>
      <td><a href="/extended-syntax/#heading-ids">Heading ID</a></td>
      <td><code>### My Great Heading {#custom-id}</code></td>
    </tr>
    <tr>
      <td><a href="/extended-syntax/#definition-lists">Definition List</a></td>
      <td><code>
        term<br />
        : definition
      </code></td>
    </tr>
    <tr>
      <td><a href="/extended-syntax/#strikethrough">Strikethrough</a></td>
      <td><code>~~The world is flat.~~</code></td>
    </tr>
    <tr>
      <td><a href="/extended-syntax/#task-lists">Task List</a></td>
      <td><code>
        - [x] Write the press release<br />
        - [ ] Update the website<br />
        - [ ] Contact the media
      </code></td>
    </tr>
    <tr>
      <td><a href="/extended-syntax/#emoji">Emoji</a><br />(see also <a href="/extended-syntax/#copying-and-pasting-emoji">Copying and Pasting Emoji</a>)</td>
      <td><code>
        That is so funny! :joy:
      </code></td>
    </tr>
    <tr>
      <td><a href="/extended-syntax/#highlight">Highlight</a></td>
      <td><code>
        I need to highlight these ==very important words==.
      </code></td>
    </tr>
    <tr>
      <td><a href="/extended-syntax/#subscript">Subscript</a></td>
      <td><code>
        H~2~O
      </code></td>
    </tr>
    <tr>
      <td><a href="/extended-syntax/#superscript">Superscript</a></td>
      <td><code>
        X^2^
      </code></td>
    </tr>
  </tbody>
</table>

<h2 id="downloads">Downloads</h2>

<p>You can <a href="/assets/markdown-cheat-sheet.md" download="markdown-cheat-sheet.md">download this cheat sheet as a Markdown file</a> for use in your Markdown application.</p>

          <div class="card bg-light mb-3" style="max-width: 100%; margin-top: 50px;">
  <div class="row no-gutters">
    <div class="col-md-3">
      <a href="/book/">
        
<img srcset="https://mdg.imgix.net/assets/images/book-cover.jpg?auto=format&fit=clip&w=480 480w,
             https://mdg.imgix.net/assets/images/book-cover.jpg?auto=format&fit=clip&q=40&w=1080 1080w"
             src="https://mdg.imgix.net/assets/images/book-cover.jpg" class="img-fluid"   alt="Markdown Guide book cover" loading="lazy" sizes="100vw">

      </a>
    </div>
    <div class="col-md-9">
      <div class="card-body" style="padding-left: 6%;">
        <h5 class="card-title no-anchor" data-toc-skip>Take your Markdown skills to the next level.</h5>
        <p class="card-text">Learn Markdown in 60 pages. Designed for both novices and experts, <em>The Markdown Guide</em> book is a comprehensive reference that has everything you need to get started and master Markdown syntax.</p>
        <a class="btn btn-success" style="margin-top: 5px; margin-right: 15px" href="/book/">Get the Book</a>
      </div>
    </div>
  </div>
</div>

<div class="card border-info" style="margin-top: 50px">
  <h6 class="card-header no-anchor bg-info text-white" data-toc-skip>Want to learn more Markdown?</h6>
  <div class="card-body">
    <p class="card-text">
      Don't stop now! 🚀 Star the <a href="https://github.com/mattcone/markdown-guide">GitHub repository</a> and then enter your email address below to receive new Markdown tutorials via email. No spam!</p>

      <form method="post" action="https://pika.forklabs.com/subscription/form" class="listmonk-form">
      <div class="form-group" style="margin-top: 20px">
        <input type="hidden" name="nonce" />
        <input id="b7668" type="hidden" name="l" value="b766881d-27fd-43a8-9b76-1b74f2a76763" />
        <input type="email" class="form-control form-control-lg" name="email" placeholder="Your email address" />
      </div>
      <button type="submit" class="btn btn-success">Stay updated</button>
    </form>

  </div>
</div>
        </div>
        <div class="col-md-3 d-none d-md-block" id="sidebar">
          <nav id="toc" data-toggle="toc" class="sticky-top" style="z-index:1"></nav>
          
          <div style="position: fixed; bottom: 0px; right: 0px;">
            <div data-ea-publisher="markdownguide" data-ea-type="image"></div>
          </div>
          
        </div>
      </div>
    </div>
    <footer class="site-footer">
  <div class="container">
    <p style="font-size: 14px; font-weight: 600"><a href="/about/">About</a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="/contact/">Contact</a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="https://github.com/mattcone/markdown-guide">GitHub</a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="/api/v1/">API</a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="/privacy-policy/">Privacy Policy</a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="/terms-and-conditions/">Terms and Conditions</a></p>
    <p style="margin-top: 15px; font-size: 14px;">&#169; 2025. A <a href="https://www.mattcone.com">Matt Cone</a> project. <a rel="license" href="https://creativecommons.org/licenses/by-sa/4.0/">CC BY-SA 4.0</a>. Made with 🌶️ in <a href="https://www.newmexico.org/">New Mexico</a>.</p>
  </div>
</footer>
<script type="text/javascript" src="/assets/javascript/anchor.min.js"></script>
<script>
  anchors.options = {
  placement: 'right',
  };
  anchors.add('h1, h2, h3, h4, h5').remove('.no-anchor');
</script>

<!-- for search -->
<script src="/assets/javascript/docsearch.min.js"></script>
<script>
docsearch({
container: '#docsearch',
appId: 'G2ZRR9A979',
apiKey: 'f522befe6142f2279344e025a79539a7',
indexName: 'markdownguide',
inputSelector: '#search-input',
debug: false // Set debug to true if you want to inspect the dropdown
});
</script>

  </body>
</html>
