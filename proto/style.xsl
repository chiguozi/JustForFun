<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:msxsl= "urn:schemas-microsoft-com:xslt "   
xmlns:user= "whatever ">   

<!-- 对type的处理 -->
<xsl:template match="type" name="type">
  <xsl:if test="count(.) > 0" >
    <div class="type">
    <h3>类型: <xsl:value-of select="@name"/> (<xsl:value-of select="@desc"/>)<a class="backToIndex" href="#index" target="_self">回目录</a></h3>
    <table cellspacing="0">
    <tr>
     <th width="80px">类型</th>
     <th width="80px">循环</th>
     <th width="80px">名称</th>
     <th >描述</th>
    </tr>
    <xsl:for-each select="*">
      <xsl:choose>
        <xsl:when test="name(.) = 'f'">
          <tr>
          <td><xsl:value-of select="@t" /></td>
          <td>否</td>
          <td><xsl:value-of select="@name" /></td>
          <td><xsl:value-of select="@desc" /></td>
          </tr>
        </xsl:when>
        <xsl:when test="name(.) = 'loop'">
          <tr>
          <td><xsl:value-of select="@t" /></td>
          <td>是</td>
          <td><xsl:value-of select="@name" /></td>
          <td><xsl:value-of select="@desc" /></td>
          </tr>
        </xsl:when>
      </xsl:choose>
    </xsl:for-each>
    </table>
    </div>
  </xsl:if>
</xsl:template>

<!-- 对msg.c2s的处理 -->
<xsl:template match="c2s" name="msg_c2s">
    <div class="msg_c2s">
    <h1>C->S:</h1>
    <table cellspacing="0">
    <tr>
     <th width="80px">类型</th>
     <th width="50px">循环</th>
     <th width="80px">名称</th>
     <th >描述</th>
    </tr>
    <!-- 判断是否包含字段 -->
    <xsl:choose>
        <xsl:when test="count(./*) = 0">
            <tr>
                <td colspan="4">无字段</td>
            </tr>
        </xsl:when>
        <xsl:otherwise>
            <xsl:for-each select="*" >
              <xsl:choose>
                <xsl:when test="name(.) = 'f'">
                  <tr>
					<td>
						<xsl:if test="substring(@t,0,3) = 'p_'">
							<a>
								<xsl:attribute name="href">#<xsl:value-of select="@t" /></xsl:attribute>
								<xsl:value-of select="@t" />
							</a>
						</xsl:if>
						<xsl:if test="substring(@t,0,3) != 'p_'">
							<xsl:value-of select="@t" />
						</xsl:if>
					</td>
                  <td>否</td>
                  <td><xsl:value-of select="@name" /></td>
                  <td><xsl:value-of select="@desc" /></td>
                  </tr>
                </xsl:when>
                <xsl:when test="name(.) = 'loop'">
                  <tr>
					<td>
						<xsl:if test="substring(@t,0,3) = 'p_'">
							<a>
								<xsl:attribute name="href">#<xsl:value-of select="@t" /></xsl:attribute>
								<xsl:value-of select="@t" />
							</a>
						</xsl:if>
						<xsl:if test="substring(@t,0,3) != 'p_'">
							<xsl:value-of select="@t" />
						</xsl:if>
					</td>
                  <td>是</td>
                  <td><xsl:value-of select="@name" /></td>
                  <td><xsl:value-of select="@desc" /></td>
                  </tr>
                </xsl:when>
              </xsl:choose>
            </xsl:for-each>
        </xsl:otherwise>
    </xsl:choose>
    </table>
    </div>
</xsl:template>

<!-- 对msg.s2c的处理 -->
<xsl:template match="s2c" name="msg_s2c">
    <div class="msg_s2c">
    <h1>S->C:</h1>
    <table cellspacing="0">
    <tr>
     <th width="80px">类型</th>
     <th width="50px">循环</th>
     <th width="80px">名称</th>
     <th >描述</th>
    </tr>
    <!-- 判断是否包含字段 -->
    <xsl:choose>
        <xsl:when test="count(./*) = 0">
            <tr>
                <td colspan="4">无字段</td>
            </tr>
        </xsl:when>
        <xsl:otherwise>
            <xsl:for-each select="*" >
              <xsl:choose>
			  <!--
                <xsl:when test="name(.) = 'f'">
                  <tr>
                  <td><xsl:value-of select="@t" /></td>
                  <td>否</td>
                  <td><xsl:value-of select="@name" /></td>
                  <td><xsl:value-of select="@desc" /></td>
                  </tr>
                </xsl:when>
			-->
			<xsl:when test="name(.) = 'f'">
                  <tr>
					<td>
						<xsl:if test="substring(@t,0,3) = 'p_'">
							<a>
								<xsl:attribute name="href">#<xsl:value-of select="@t" /></xsl:attribute>
								<xsl:value-of select="@t" />
							</a>
						</xsl:if>
						<xsl:if test="substring(@t,0,3) != 'p_'">
							<xsl:value-of select="@t" />
						</xsl:if>
					</td>
                  <td>否</td>
                  <td><xsl:value-of select="@name" /></td>
                  <td><xsl:value-of select="@desc" /></td>
                  </tr>
                </xsl:when>
                <xsl:when test="name(.) = 'loop'">
                  <tr>
					<td>
						<xsl:if test="substring(@t,0,3) = 'p_'">
							<a>
								<xsl:attribute name="href">#<xsl:value-of select="@t" /></xsl:attribute>
								<xsl:value-of select="@t" />
							</a>
						</xsl:if>
						<xsl:if test="substring(@t,0,3) != 'p_'">
							<xsl:value-of select="@t" />
						</xsl:if>
					</td>
                  <td>是</td>
                  <td><xsl:value-of select="@name" /></td>
                  <td><xsl:value-of select="@desc" /></td>
                  </tr>
                </xsl:when>
              </xsl:choose>
            </xsl:for-each>
        </xsl:otherwise>
    </xsl:choose>
    </table>
    </div>
</xsl:template>

<!-- 匹配整个xml -->
<xsl:template match="/">
  <html>
  <style type= "text/css">

      .type {
        margin:10px;
      }
      .type table{
          border:1px solid #99ccff;
      }
      .type td{
		padding:3px;
		border-bottom:1px solid #99ccff;
		border-right:1px solid #99ccff;
		border-left:1px solid #99ccff;
      }
      .type th{
        padding:4px;
        background-color:#99ccff;
      }

      .msg {
        margin:20px 5px 10px;
      }
      .msg .id{
          color:#208822;
          font-weight:bold;
          margin-right:8px;
      }
      .msg .desc{
          font-weight:bold;
          margin-right:8px;
      }

      .msg_c2s {
        margin-top:3px;
        margin-left:10px;
      }
      .msg_c2s h1{
        font-size:15px;
        font-weight:bold;
        color:#339900;
      }
      .msg_c2s table{
          border:1px solid #66cc00;
      }
      .msg_c2s td{
		padding:3px;
		border-bottom:1px solid #66cc00;
		border-right:1px solid #66cc00;
		border-left:1px solid #66cc00;
      }
      .msg_c2s th{
        padding:4px;
        background-color:#66cc00;
      }

      .msg_s2c {
        margin-top:3px;
        margin-left:10px;
      }
      .msg_s2c h1{
        font-size:15px;
        font-weight:bold;
        color:#ff6600;
        margin:0;
        padding:0;
      }
      .msg_s2c table{
          border:1px solid #ff9900;
      }
      .msg_s2c td{
		padding:3px;
		border-bottom:1px solid #ff9900;
		border-right:1px solid #ff9900;
		border-left:1px solid #ff9900;
      }
      .msg_s2c th{
        padding:4px;
        background-color:#ff9900;
      }

	.loopTd td{
		padding:2px;
		padding-left:20px;
		color:#990033;
	}
	.backToIndex{
		margin-left:30px;
		text-decoration:none;
		color:#208822;font-weight:bold;margin-right:8px; border:1px solid #666633;
	}
	.index_title{
		text-decoration:none;
		padding:0px;
		margin:1px; margin-top:3px;
		font-size:16px;font-weight:bold;
        color:#700023;font-weight:bold;margin-right:8px;
	}
	.index_item{
		padding:0px; padding-left:20px;
		margin:1px;
	}
	.index_item a{
		text-decoration:none;
		font-size:12px;
        color:#3a54a3;font-weight:normal;margin-right:8px;
	}
	.index_item span{
		color:#503e53;font-weight:normal;margin-left:5px;
	}
  </style> 
  <body>
  <h1>协议文档</h1>
  <h2>协议说明</h2>
  <div>
  <p>
        [2字节长度][1字节消息分类][1字节消息id][消息数据]<br/>
        所有数据采用big-endian(网络字节序号)
   </p>
    <p>
        int64: 为8字节<br/>
        int32: 为4字节<br/>
        int16: 为2字节<br/>
        int8: 为1字节<br/>
        string: 表示方式为 [2字节长度][字符串]<br/>
        loop节点表示里面内容进行循环，loop为2字节<br/>
    </p>
    <p> 
    对于请求(c2s)的应答(s2c)，直接将请求中的序列号返回。对于服务器主动
    推送的数据的，序列号为0 
    </p>
    <p>
        section 表明消息分类,type属性为此类消息的标识<br/>
    msg 为具体的一个消息，包含c2s（请求）和s2c（应答），有些消息包含一个请求一个 应答，有的消息没有请求只有应答（服务器推送）。<br/>
    c2s为请求，id为消息id，&lt;f&gt;为对应的字段。<br/>
    s2c为应答，id为消息id，&lt;f&gt;定义同c2s。
    </p>
    <p>
        <b>定义协议时推荐字段顺序(从上到下):</b>
        <ol>
             <li>基本类型(int, string)</li>
             <li>自定义类型</li>
             <li>loop</li>
        </ol>
        这样效率会比较高！
    </p>
  </div>

	<a name="index"/>
	<!--目录处理 -->
	<div style="background-color:#eeeeee; width:600px; padding:10px; padding-left:20px;">
	<h2>目录</h2>
    <!--自定义结构-->
    <a class="index_title" target="_self">
        自定义结构
    </a>

    <xsl:for-each select="protocol/custom_type/type"> 
    	<xsl:variable name="struct_name" select="@name">
    	</xsl:variable>
			<p class="index_item">
				<a>
					<xsl:attribute name="href">#<xsl:value-of select="$struct_name"/></xsl:attribute>
					自定义结构：[
					<xsl:value-of select="@desc"/>]
					<span>
						----------------[<xsl:value-of select="$struct_name"/>]
					</span>
				</a>
			</p>
    </xsl:for-each>

	<xsl:for-each select="protocol/section">
        <a class="index_title" target="_self">
			<xsl:attribute name="href">#<xsl:value-of select="@id"/></xsl:attribute><xsl:value-of select="@desc"/>
			分类Id:<xsl:value-of select="@id" />
        </a>
        <xsl:variable name="section_id" select="@id">
        </xsl:variable>
		<!-- 显示消息 -->
        <xsl:for-each select="msg"> 
        	<xsl:variable name="sub_id" select="@id">
        	</xsl:variable>
				<p class="index_item">
					<a>
						<xsl:attribute name="href">#<xsl:value-of select="$section_id * 256 + $sub_id"/></xsl:attribute>
						(<xsl:value-of select="$section_id"/>,<xsl:value-of select="@id"/>)
						<xsl:value-of select="@desc"/>
						<span>
							----------------[<xsl:value-of select="$section_id * 256 + $sub_id"/>]
						</span>
					</a>
				</p>
        </xsl:for-each>
    </xsl:for-each>
	</div>

    <!--关于自定义协议 -->
    <xsl:for-each select="protocol/custom_type/type"> 
        <xsl:variable name="struct_name" select="@name"></xsl:variable>
        <a>
	    	<xsl:attribute name="name"><xsl:value-of select="$struct_name"/></xsl:attribute>
	    	<xsl:attribute name="id"><xsl:value-of select="$struct_name"/></xsl:attribute>
	    </a>
        <xsl:call-template name="type"/>
    </xsl:for-each>

    <!--关于各个协议部分 -->
    <xsl:for-each select="protocol/section">
		<a><xsl:attribute name="name"><xsl:value-of select="@id"/></xsl:attribute><xsl:attribute name="id"><xsl:value-of select="@id"/></xsl:attribute></a>
        <h2><xsl:value-of select="@desc"/>
		 分类Id:<xsl:value-of select="@id" />
        </h2>
        <xsl:variable name="section_id" select="@id"></xsl:variable>
        <xsl:for-each select="*">
			<xsl:choose>
			<!-- 消息定义 -->
            <xsl:when test="name(.) = 'msg'">
                <xsl:variable name="sub_id" select="@id"></xsl:variable>
                <a>
                    <xsl:attribute name="name"><xsl:value-of select="$section_id * 256 + $sub_id"/></xsl:attribute>
                    <xsl:attribute name="id"><xsl:value-of select="$section_id * 256 + $sub_id"/></xsl:attribute>
                </a>
                <div class="msg">ID:
                <span class="id">(<xsl:value-of select="$section_id"/>-<xsl:value-of select="@id"/>)</span>
                <span class="id">(<xsl:value-of select="@name"/>)</span>
                <span class="desc"><xsl:value-of select="@desc"/></span>
                <a class="backToIndex" target="_self">
                    <xsl:attribute name="href">#<xsl:value-of select="$section_id"/></xsl:attribute>
                    回分类头部
                </a>
                <a class="backToIndex" href="#index" target="_self">回目录</a>
                
                <xsl:for-each select="*">
                    <xsl:choose>
                        <!-- 处理c2s -->
                        <xsl:when test="name(.) ='c2s'">
                            <xsl:call-template name="msg_c2s"/>
                        </xsl:when>

                        <!-- 处理s2c -->
                        <xsl:when test="name(.) ='s2c'">
                            <xsl:call-template name="msg_s2c"/>
                        </xsl:when>
                    </xsl:choose>
                </xsl:for-each>
                </div>
            </xsl:when>
        </xsl:choose>
        </xsl:for-each>
    </xsl:for-each>
	</body>
	</html>
</xsl:template>

</xsl:stylesheet>
