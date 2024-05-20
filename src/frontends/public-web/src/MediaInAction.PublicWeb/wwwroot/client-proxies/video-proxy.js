/* This file is automatically generated by ABP framework to use MVC Controllers from javascript. */


// module video

(function(){

  // controller MediaInAction.videoService.orders.order

  (function(){

    abp.utils.createNamespace(window, 'MediaInAction.videoService.seriess.series');

    MediaInAction.videoService.seriess.series.get = function(id, ajaxParams) {
      return abp.ajax($.extend(true, {
        url: abp.appPath + 'api/video/series/' + id + '',
        type: 'GET'
      }, ajaxParams));
    };

    MediaInAction.videoService.orders.order.getMyOrders = function(input, ajaxParams) {
      return abp.ajax($.extend(true, {
        url: abp.appPath + 'api/video/series/my-orders' + abp.utils.buildQueryString([{ name: 'filter', value: input.filter }]) + '',
        type: 'GET'
      }, ajaxParams));
    };

    MediaInAction.videoService.orders.order.getByOrderNo = function(orderNo, ajaxParams) {
      return abp.ajax($.extend(true, {
        url: abp.appPath + 'api/video/order/by-order-no' + abp.utils.buildQueryString([{ name: 'orderNo', value: orderNo }]) + '',
        type: 'GET'
      }, ajaxParams));
    };

    MediaInAction.videoService.orders.order.create = function(input, ajaxParams) {
      return abp.ajax($.extend(true, {
        url: abp.appPath + 'api/video/order',
        type: 'POST',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

  })();

})();

