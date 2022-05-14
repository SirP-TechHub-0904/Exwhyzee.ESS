jQuery(window).load(function(){
	'use strict';
	if ( typeof $.cookie('trx-sale-popup-show') === 'undefined') { 
		jQuery('.trx_sale_popup').show();
		jQuery('.trx_sale_popup_close').on('click', function(){
			jQuery('.trx_sale_popup').hide();
			jQuery.cookie('trx-sale-popup-show', false);
		});
	}	
});