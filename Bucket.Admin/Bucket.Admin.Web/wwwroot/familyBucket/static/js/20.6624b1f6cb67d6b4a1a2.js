webpackJsonp([20],{dKBV:function(e,t){},inBm:function(e,t,o){"use strict";var r=o("Xxa5"),l=o.n(r),n=o("exGp"),a=o.n(n),i=o("Zx67"),s=o.n(i),m=o("Zrlr"),u=o.n(m),d=o("wxAW"),c=o.n(d),p=o("zwoO"),f=o.n(p),v=o("Pf15"),h=o.n(v),b=o("hky6"),F=function(e){function t(){return u()(this,t),f()(this,(t.__proto__||s()(t)).apply(this,arguments))}return h()(t,e),c()(t,[{key:"search",value:function(){var e=a()(l.a.mark(function e(){var t;return l.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,b.a.QueryPlatforms();case 2:(t=e.sent)&&(this.vm.dataList=t.Data);case 4:case"end":return e.stop()}},e,this)}));return function(){return e.apply(this,arguments)}}()},{key:"add",value:function(){this.vm.editModel={Name:null,Key:null,Icon:null,Author:null,Developer:null,SortId:0,Remark:null,IsDel:!1},this.vm.showEdit=!0}},{key:"editSubmit",value:function(){var e,t=this;this.vm.$refs.modelForm.validate((e=a()(l.a.mark(function e(o){var r;return l.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:if(!o){e.next=8;break}return r={loadID:"edit"},e.next=4,b.a.SetPlatformInfo(t.vm.modelForm,r);case 4:e.sent&&(t.vm.show=!1,t.vm.$emit("addSuccess"),t.vm.$notify({title:"成功",message:"操作成功",type:"success"})),e.next=9;break;case 8:return e.abrupt("return",!1);case 9:case"end":return e.stop()}},e,t)})),function(t){return e.apply(this,arguments)}))}}]),t}(o("ARvU").a);t.a=F},msQy:function(e,t,o){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=o("inBm"),l={props:{value:{type:Boolean,default:!1},modelForm:{type:Object}},data:function(){return{data:[],show:!1,btnName:"新建",rules:{Name:[{required:!0,message:"请输入平台名称",trigger:"blur"}],Key:[{required:!0,message:"请输入平台Key",trigger:"blur"}]}}},created:function(){this.BLL=new r.a(this),this.value&&(this.show=!0)},computed:{loading:function(){return this.$store.getters.btnLoading.str&&"edit"===this.$store.getters.btnLoading.id}},mounted:function(){},methods:{handleReset:function(){this.$refs.modelForm.resetFields()},handleSubmit:function(e){this.BLL.editSubmit(e)}},watch:{show:function(e){var t=this;this.$emit("input",e),e?this.$nextTick(function(){t.data=[],t.modelForm.Id?t.btnName="编辑":t.btnName="新建"}):this.$refs.modelForm.clearValidate()},value:function(e){this.show=e}}},n={render:function(){var e=this,t=e.$createElement,o=e._self._c||t;return o("el-dialog",{attrs:{title:e.btnName,visible:e.show,"close-on-click-modal":!1,width:"50%"},on:{"update:visible":function(t){e.show=t}}},[e.modelForm?o("el-form",{directives:[{name:"loading",rawName:"v-loading",value:e.loading,expression:"loading"}],ref:"modelForm",attrs:{model:e.modelForm,rules:e.rules,"label-width":"120px"}},[o("el-form-item",{attrs:{label:"平台名称",prop:"Name"}},[o("el-input",{model:{value:e.modelForm.Name,callback:function(t){e.$set(e.modelForm,"Name",t)},expression:"modelForm.Name"}})],1),e._v(" "),o("el-form-item",{attrs:{label:"平台Key",prop:"Key"}},[o("el-input",{model:{value:e.modelForm.Key,callback:function(t){e.$set(e.modelForm,"Key",t)},expression:"modelForm.Key"}})],1),e._v(" "),o("el-form-item",{attrs:{label:"平台负责人",prop:"Author"}},[o("el-input",{model:{value:e.modelForm.Author,callback:function(t){e.$set(e.modelForm,"Author",t)},expression:"modelForm.Author"}})],1),e._v(" "),o("el-form-item",{attrs:{label:"平台开发者",prop:"Developer"}},[o("el-input",{model:{value:e.modelForm.Developer,callback:function(t){e.$set(e.modelForm,"Developer",t)},expression:"modelForm.Developer"}})],1),e._v(" "),o("el-form-item",{attrs:{label:"排序",prop:"SortId"}},[o("el-input-number",{attrs:{min:1},model:{value:e.modelForm.SortId,callback:function(t){e.$set(e.modelForm,"SortId",t)},expression:"modelForm.SortId"}})],1),e._v(" "),o("el-form-item",{attrs:{label:"备注",prop:"Remark"}},[o("el-input",{attrs:{type:"textarea"},model:{value:e.modelForm.Remark,callback:function(t){e.$set(e.modelForm,"Remark",t)},expression:"modelForm.Remark"}})],1),e._v(" "),o("el-form-item",{attrs:{label:"是否删除",prop:"IsDel"}},[o("el-switch",{model:{value:e.modelForm.IsDel,callback:function(t){e.$set(e.modelForm,"IsDel",t)},expression:"modelForm.IsDel"}})],1),e._v(" "),o("el-form-item",[o("el-button",{attrs:{type:"primary"},on:{click:e.handleSubmit}},[e._v(e._s(e.btnName))]),e._v(" "),o("el-button",{on:{click:e.handleReset}},[e._v("重置")])],1)],1):e._e()],1)},staticRenderFns:[]};var a=o("VU/8")(l,n,!1,function(e){o("dKBV")},null,null);t.default=a.exports}});