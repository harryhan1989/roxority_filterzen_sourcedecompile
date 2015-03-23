var roxMulSels = {}, roxAcHint = '', roxAjax14Interval = 0, roxMulInt, roxAjax14Hide = false, roxAjax14Focus = true, roxAutoReposts = [], roxLvDone = [], roxIsDtUndefined = false, roxDtInt = 0, roxMultiLastVals = {}, roxMultis = {}, roxLox = {}, roxMultiOpsAll = '', roxMultiOpsAny = '', roxMultiOpsNone = '', roxMultiOpsNum = '', roxMultiOpsStr = '', roxMultiOpsUser = '', roxMultiDateCounts = {}, roxMultiUserCounts = {}, roxSeps = [';#'], roxNuAction = null, roxListViews = [], roxMultiSelectBusy = false, roxMainForm = document.getElementById('aspnetForm'), roxMultiMins = {}, roxListViewUrl = '', roxFilterDescs = {}, filterListBoxID, nameTextBoxID, wizardFieldDropDownListID, roxNewQueryString = null, roxUpdatePreview = function () { }, roxButtonIds = ['#ctl00_MSOTlPn_EditorZone_MSOTlPn_OKBtn', '#ctl00_MSOTlPn_EditorZone_MSOTlPn_AppBtn', '#ctl00_MSO_ContentDiv_MSOTlPn_EditorZone_MSOTlPn_OKBtn', '#ctl00_MSO_ContentDiv_MSOTlPn_EditorZone_MSOTlPn_AppBtn'], roxFilterNames = {}, roxFilterCamlOps = {}, roxFilterEmpties = [], roxLastDateCtls = {}, roxLastUserCtls = {}, roxHideDatasheetRibbon = false, roxAnim = '/_layouts/images/roxority_FilterZen/k.gif';

try {
    if (g_strDateTimeControlIDs === undefined)
        roxIsDtUndefined = true;
} catch (e) {
    roxIsDtUndefined = true;
}
if (roxIsDtUndefined)
    g_strDateTimeControlIDs = new Array();

jQuery(document).ready(function () {
    var tab, att, node, pos, elem, elems, vals = [], subVals, sv, $td, tmp, thNum, thNames = [], thChildren, thHtml, thFids = {};
    setInterval(roxNoWarn, 250);
    if (!jQuery.browser.msie)
        jQuery('.rox-ifilter-label-datetime').css({ 'padding-bottom': '8px' });
    if (roxNewQueryString && roxMainForm) {
        if (!roxMainForm.action)
            roxMainForm.action = roxNewQueryString;
        else if ((pos = roxMainForm.action.indexOf('?')) > 0)
            roxMainForm.action = roxMainForm.action.substr(0, pos) + roxNewQueryString;
        else
            roxMainForm.action = roxMainForm.action + roxNewQueryString;
        jQuery('#aspnetForm').attr('action', roxNuAction = roxMainForm.action);
        roxRefreshFilters();
    }
    for (var i = 0; i < roxListViews.length; i++) {
        if ((jQuery.inArray(roxListViews[i].listID + '-' + roxListViews[i].viewID, roxLvDone) < 0) && ((tab = document.getElementById(roxListViews[i].listID + '-' + roxListViews[i].viewID)) || (tab = roxFindDocLibTable(roxListViews[i].viewID)))) {
            roxLvDone.push(roxListViews[i].listID + '-' + roxListViews[i].viewID);
            if (roxListViews[i].highlight && roxListViews[i].filters && roxListViews[i].filters.length)
                for (var fid in roxFilterNames) {
                    for (var s = 0; s < roxSeps.length; s++)
                        if ((fid.substr(0, roxListViews[i].wpID.length) == roxListViews[i].wpID) && (jQuery.inArray(roxFilterNames[fid], roxListViews[i].filters) >= 0) && ((elem = document.getElementById('filterval' + fid.substr(roxListViews[i].wpID.length))) != null) && elem.value && (elem.value != '0478f8f9-fbdc-42f5-99ea-f6e8ec702606') && ((elem.value != '0') || (elem.tagName.toLowerCase() != 'select')) && (subVals = elem.value.split(roxSeps[s])) && subVals.length) {
                            if (elem.className == 'rox-multiselect ms-input')
                                jQuery(elem).find('option').each(function (optIndex, option) {
                                    if (option.selected && (option.value) && (option.value != '0') && (option.value != '0478f8f9-fbdc-42f5-99ea-f6e8ec702606') && (subVals = option.value.split(roxSeps[s])) && subVals.length)
                                        if ((s == 0) && (jQuery.inArray(sv = (subVals[(subVals.length > 1) ? 1 : 0] + '').toLowerCase(), vals) < 0))
                                            vals.push(sv);
                                        else if (s > 0)
                                            for (var si = 0; si < subVals.length; si++)
                                                if (jQuery.inArray(sv = (subVals[si] + '').toLowerCase(), vals) < 0)
                                                    vals.push(sv);
                                });
                            else if ((s == 0) && (jQuery.inArray(sv = (subVals[(subVals.length > 1) ? 1 : 0] + '').toLowerCase(), vals) < 0))
                                vals.push(sv);
                            else if (s > 0)
                                for (var si = 0; si < subVals.length; si++)
                                    if (jQuery.inArray(sv = (subVals[si] + '').toLowerCase(), vals) < 0)
                                        vals.push(sv);
                        }
                }
            if (vals.length) {
                for (var v = vals.length - 1; v >= 0; v--)
                    if (((tmp = jQuery.inArray(vals[v], vals)) >= 0) && (tmp < v))
                        vals.splice(tmp, 1);
                jQuery(tab).find('td').each(function (ix, td) {
                    $td = jQuery(td);
                    for (var v = 0; v < vals.length; v++)
                        if ((vals[v] != 'on') && (($td.attr('class') + '').substr(0, 5) == 'ms-vb') && ($td.text().toLowerCase().indexOf(vals[v]) >= 0))
                            roxHighlightNode(td, vals[v]);
                });
            }
            if (roxListViews[i].disableFilters && roxListViews[i].filters && roxListViews[i].filters.length && tab.firstChild && tab.firstChild.firstChild && tab.firstChild.firstChild.childNodes && tab.firstChild.firstChild.childNodes.length)
                for (var c = 0; c < tab.firstChild.firstChild.childNodes.length; c++)
                    if (tab.firstChild.firstChild.childNodes[c].firstChild && (node = tab.firstChild.firstChild.childNodes[c].firstChild.firstChild) && node.attributes && (att = node.attributes.getNamedItem('Name')) && (jQuery.inArray(att.value, roxListViews[i].filters) >= 0))
                        jQuery(node).attr('FilterDisable', 'TRUE').attr('Filterable', 'FALSE');
                    else if ((node = tab.firstChild.firstChild.childNodes[c].firstChild) && node.attributes && (att = node.attributes.getNamedItem('Name')) && (jQuery.inArray(att.value, roxListViews[i].filters) >= 0))
                        jQuery(node).attr('FilterDisable', 'TRUE').attr('Filterable', 'FALSE');
            if (roxListViews[i].embedFilters) {
                thChildren = jQuery(tab.firstChild.firstChild).children();
                thNum = thChildren.length;
                thHtml = '<tr>';
                for (var th = 0; th < thNum; th++) {
                    if ((!(thNames[th] = jQuery(thChildren[th].firstChild.firstChild).attr("Name"))) && (!(thNames[th] = jQuery(thChildren[th].firstChild).attr("Name"))))
                        thNames[th] = '';
                    thHtml += '<td class="ms-vh2" id="';
                    if (thNames[th])
                        for (var fid in roxFilterNames)
                            if ((fid.substr(0, roxListViews[i].wpID.length) == roxListViews[i].wpID) && (roxFilterNames[fid] == thNames[th])) {
                                thHtml += 'rfzembed' + fid.substr(roxListViews[i].wpID.length);
                                thFids[fid.substr(roxListViews[i].wpID.length)] = roxListViews[i].wpID;
                                break;
                            }
                    thHtml += '"></td>';
                }
                thHtml += '</tr>';
                jQuery(tab.firstChild.firstChild).after(thHtml);
                for (var thFid in thFids) {
                    jQuery('#rox_ifilter_control' + thFid).prependTo('#rfzembed' + thFid);
                    jQuery('.rox-ifilter-all-' + thFids[thFid]).hide();
                }
            }
        }
    }
    jQuery('span.rox-ifilter-datetime td.ms-dtinput').keyup(roxOnKey);
    jQuery('span.rox-ifilter input, span.rox-ifilter select').change(function () {
        jQuery(this).parents('span.rox-ifilter').addClass('rox-ifilter-stale');
        jQuery(this).parents('span.rox-ifilter-all').addClass('rox-ifilter-all-stale');
    });
    if (roxAjax14Interval)
        setInterval(roxAjax14EmulateListAutoRefresh, roxAjax14Interval * 1000);

    inputTipText();
});

jQuery(window).load(function () {
    var tab, node, att, mulSels;
    for (var i = 0; i < roxListViews.length; i++)
        if (roxListViews[i].filters && roxListViews[i].filters.length && (tab = document.getElementById(roxListViews[i].listID + '-' + roxListViews[i].viewID)) && tab.firstChild && tab.firstChild.firstChild && tab.firstChild.firstChild.childNodes && tab.firstChild.firstChild.childNodes.length)
            for (var c = 0; c < tab.firstChild.firstChild.childNodes.length; c++)
                if (tab.firstChild.firstChild.childNodes[c].firstChild && (node = tab.firstChild.firstChild.childNodes[c].firstChild.firstChild) && node.attributes && (att = node.attributes.getNamedItem('Name')) && (jQuery.inArray(att.value, roxListViews[i].filters) >= 0))
                    jQuery(node).attr('FilterDisable', 'TRUE');
    roxNoWarn();
    if ((mulSels = jQuery('span.rox-ifilter input.multiSelect')).length) {
        mulSels.each(function () {
            roxMulSels[this.parentNode.id] = jQuery(this).val();
        });
        roxMulInt = setInterval(roxMulSelCheck, 500);
    }
    jQuery('span.rox-ifilter-label2').each(function (i, span) {
        var fid = span.id.substr("rox_ifilter_label2_".length), ctl1 = document.getElementById('roxifiltercontrol1_' + fid);
        if (ctl1)
            jQuery('#rox_ifilter_label_' + fid).width(jQuery(ctl1).width());
    });
});

function roxAjax14EmulateListAutoRefresh() {
    if (roxAjax14Focus && !$(".roxfilterouter *:focus").length)
        return;
    jQuery('img#ManualRefresh').click();
}

function roxMulSelCheck() {
    var el, txt, refr = '';
    for (var p in roxMulSels)
        if (((el = jQuery('#' + p)).length) && ((txt = el.find('input.multiSelect')).length) && (txt.val() != roxMulSels[p])) {
            txt.parents('span.rox-ifilter').addClass('rox-ifilter-stale');
            txt.parents('span.rox-ifilter-all').addClass('rox-ifilter-all-stale');
            for (var i = 0; i < roxAutoReposts.length; i++)
                if (roxAutoReposts[i] == 'rox_ifilter_' + p.substr(p.lastIndexOf('_') + 1)) {
                    refr = el.parent().removeClass('rox-ifilter-control').attr('class').substr('rox-ifilter-control-'.length);
                    break;
                }
        }
    if (refr) {
        if (roxMulInt)
            clearInterval(roxMulInt);
        roxRefreshFilters(refr);
    }
}

function roxFindDocLibTable(viewID) {
    var tab = null;
    jQuery('table.ms-listviewtable, table.ms-summarystandardbody').each(function (i, table) {
        var $t = jQuery(table), attVal;
        try {
            attVal = $t.attr('o:WebQuerySourceHref');
        } catch (err) {
            try {
                attVal = $t.attr('WebQuerySourceHref');
            } catch (err2) {
            }
        }
        if ((attVal && (attVal.indexOf('View=' + viewID) > 0)) || (viewID.toLowerCase() == ('{' + $t.parents('div').attr('WebPartID').toLowerCase() + '}')))
            tab = table;
    });
    return tab;
}

function roxHighlightNode(node, val) {
    var html, $node = jQuery(node), lastIndex = 0, ist;
    if ((node.tagName == 'SPAN') && ($node.attr('class') == 'rox-hilitematch'))
        return '';
    else if (node.nodeName == '#text') {
        html = node.nodeValue;
        while ((lastIndex = html.toLowerCase().indexOf(val.toLowerCase(), lastIndex)) >= 0) {
            html = ((lastIndex == 0) ? '' : html.substr(0, lastIndex)) + '<span class="rox-hilitematch">' + html.substr(lastIndex, val.length) + '</span>' + html.substr(lastIndex + val.length);
            lastIndex = lastIndex + val.length + 37;
        }
        return html;
    } else {
        for (var i = 0; i < node.childNodes.length; i++)
            if ((html = roxHighlightNode(node.childNodes[i], val)) && html.length)
                jQuery(node.childNodes[i]).replaceWith(html);
        return '';
    }
}

function roxNoWarn() {
    var ribbon, itemIDs = [];
    try {
        g_bWarnBeforeLeave = false;
    } catch (exc) {
    }
    try {
        g_warnonce = 0;
    } catch (exc) {
    }
    try {
        if (SP && SP['Ribbon'] && SP.Ribbon['PageState'] && SP.Ribbon.PageState['PageStateHandler'])
            SP.Ribbon.PageState.PageStateHandler.ignoreNextUnload = true;
    } catch (exc) {
    }
    if (roxHideDatasheetRibbon) {
        if (((ribbon = document.getElementById('Ribbon.List.ViewFormat.Datasheet-Large')) || ((ribbon = jQuery('#Ribbon.List.ViewFormat.Datasheet-Large')) && ribbon.length && (ribbon = ribbon[0]))))
            jQuery(ribbon).css({ 'display': 'none' });
        jQuery('td.ms-toolbar span menu *').each(function (i, menuItem) {
            if (menuItem.id.indexOf('_EditInGridButton') > 0)
                itemIDs.push(menuItem.id);
        });
        if (itemIDs.length)
            for (var i = 0; i < itemIDs.length; i++)
                jQuery('#' + itemIDs[i]).remove();
    }
    if (roxAjax14Hide)
        jQuery('img#ManualRefresh').hide();
}

function roxClearFilters(wpid, fid, clearAct) {
    jQuery((fid ? ('#rox_ifilter_control_' + fid) : ('span.rox-ifilter-control-' + wpid)) + ' input.ms-input').each(function (i, e) { try { jQuery(e).val(''); } catch (err) { } });
    jQuery((fid ? ('#rox_ifilter_control_' + fid) : ('span.rox-ifilter-control-' + wpid)) + ' input.multiSelect').each(function (i, e) { try { jQuery(e).val(''); } catch (err) { } });
    jQuery((fid ? ('#rox_ifilter_control_' + fid) : ('span.rox-ifilter-control-' + wpid)) + ' div.multiSelectOptions label input').each(function (i, e) { try { e.checked = (i == 0); } catch (err) { } });
    jQuery((fid ? ('#rox_ifilter_control_' + fid) : ('span.rox-ifilter-control-' + wpid)) + ' select.ms-input').each(function (i, e) { try { e.selectedIndex = 0; } catch (err) { try { e.selectedIndex = -1; } catch (err2) { } } });
    jQuery((fid ? ('#rox_ifilter_control_' + fid) : ('span.rox-ifilter-control-' + wpid)) + ' input.rox-check-value').attr('checked', false).removeAttr('checked');
    jQuery((fid ? ('#rox_ifilter_control_' + fid) : ('span.rox-ifilter-control-' + wpid)) + ' input.rox-check-default').attr('checked', true);
    jQuery((fid ? ('div.rox-multibox-' + fid) : ('span.rox-ifilter-control-' + wpid)) + ' div.rox-multifilter').each(function (i, div) {
        var cls = div.className.split(/\s+/)[1], index = cls.substr(cls.lastIndexOf('_') + 1), len = 'rox-multifilter-'.length, id = cls.substr(len, cls.length - len - (1 + index.length));
        roxMultiChanged(id, parseInt(index), 'val', '');
    });
    if (!fid)
        roxRefreshFilters(wpid, clearAct);
}

function roxCollapseGroups(wpID) {
    jQuery('table.ms-listviewtable td.ms-gb a').each(function (i, a) {
        var $a = jQuery(a);
        if ((a.href == 'javascript:') && (a.onclick.toString().indexOf("ExpCollGroup") > -1) && ($a.html().toLowerCase().indexOf('<img ') > -1))
            $a.click();
    });
}

function roxMultiSelect(elem, temp) {
    var minVal, elems, i;
    if (roxMultiSelectBusy)
        return;
    roxMultiSelectBusy = true;
    if ((!elem.id) && ((minVal = roxMultiMins[elem.attr('name')]) != undefined) && (elems = document.getElementsByName(elem.attr('name'))) && elems.length) {
        if ((minVal + '') == (elem.attr('value') + '')) {
            for (i = 0; i < elems.length; i++)
                if (((minVal + '') != (jQuery(elems[i]).attr('value') + '')) && jQuery(elems[i]).attr('checked')) {
                    jQuery(elems[i]).attr('checked', false);
                    jQuery(elems[i]).click();
                    jQuery(elems[i]).attr('checked', false);
                }
            if (!elem.attr('checked')) {
                elem.attr('checked', true);
                elem.click();
                elem.attr('checked', true);
            }
        } else {
            for (i = 0; i < elems.length; i++)
                if ((minVal + '') == (jQuery(elems[i]).attr('value') + '')) {
                    jQuery(elems[i]).attr('checked', false);
                    jQuery(elems[i]).click();
                    jQuery(elems[i]).attr('checked', false);
                }
        }
    }
    roxMultiSelectBusy = false;
}

function roxOnKey(event, wpid) {
    if (event && (event.keyCode == 13))
        roxRefreshFilters(wpid);
}

function roxRefreshFilters(wpid, clearAct) {
    roxNoWarn();
    if (wpid) {
        jQuery('#roxact_' + wpid).val('wpid_' + wpid);
        jQuery('#roxact2_' + wpid).val(clearAct ? '' : ('wpid_' + wpid));
    }
    setTimeout("showBusy();", 10);
    setTimeout("if(roxNuAction){jQuery('#aspnetForm').attr('action', roxNuAction);}if(roxMainForm)roxMainForm.submit();", 10);
}

function configInitPage(okButtonID) {
    jQuery(document).ready(function () {
        var $hidden = jQuery('#' + roxHiddenFieldID), $hidden2 = jQuery('#' + roxHidden2FieldID);
        if ($hidden2.val() != '1') {
            MSOTlPn_onToolPaneMaxClick();
            jQuery(jQuery('div.UserSectionTitle a')[0]).click();
            setTimeout("showBusy();", 10);
            $hidden2.val('1');
            if (theForm) {
                if (theForm.__EVENTTARGET) theForm.__EVENTTARGET.value = okButtonID;
                if (theForm.__EVENTARGUMENT) theForm.__EVENTARGUMENT.value = '';
                setTimeout("theForm.submit();", 500);
            }
        } else if ($hidden.val() == '1')
            toggleListWizard(roxHiddenFieldID);
    });
}

function editorUse() {
    toggleListWizard();
    jQuery('#' + nameTextBoxID).val(document.getElementById(wizardFieldDropDownListID).options[document.getElementById(wizardFieldDropDownListID).selectedIndex].value).focus();
}

function repopulateList(listID, textareaID, selVal, emptyID) {
    var tmp, textarea = document.getElementById(textareaID), tmpIndex = 1, isSel, selIndices = [], lines = jQuery(textarea).val().split("\n"), list = document.getElementById(listID), isMult = jQuery(list).attr('multiple'), selVals = (isMult ? selVal.split(',') : [selVal]), inner = '';
    if (lines && lines.length)
        for (var l = 0; l < lines.length; l++)
            if (lines[l] && lines[l].length)
                if ((tmp = jQuery.trim(lines[l])).length) {
                    inner += ('<option value=\"' + tmp + '\"' + ((isSel = (jQuery.inArray(tmp, selVals) >= 0)) ? ' selected=\"selected\"' : '') + '>' + tmp + '</option>');
                    if (isSel)
                        selIndices.push(tmpIndex);
                    tmpIndex++;
                }
    inner = '<option value=\"' + emptyID + '\"' + ((isSel = ((selVals.length == 0) || (jQuery.inArray(emptyID, selVals) >= 0))) ? ' selected=\"selected\"' : '') + '>' + roxEmpty + '</option>' + inner;
    if (isSel)
        selIndices.push(0);
    jQuery(list).html(inner);
    setTimeout(function () {
        jQuery(list).val(selVals);
    }, 25);
}

function showBusy() {
    if (roxAnim != 'NO_BLANK') {
        setTimeout("jQuery('.roxfilterouter').css({ 'background-image': \"url('" + roxAnim + "')\" });", 5);
        jQuery('.roxfilterouter').css({ 'background-image': "url('" + roxAnim + "')" });
        jQuery('#MSOTlPn_MainTD').css({ 'background': "#ffffff url('" + roxAnim + "') center center no-repeat" });
        setTimeout("jQuery('#MSOTlPn_Tbl').hide();", 5);
        jQuery('.roxfilterinner').css({ 'visibility': 'hidden' });
    }
}

function showFilterEditor(title) {
    var jq;
    jQuery('#roxfiltereditor').show();
    jQuery('#roxfilterlist').hide();
    jQuery('#roxlicsection').hide();
    jQuery('#roxeditortitle').text(title);
    jQuery(document).ready(function () {
        var done = false;
        for (var i = 0; i < roxButtonIds.length; i++)
            if ((jq = jQuery(roxButtonIds[i])) && jq.length)
                jq[0].disabled = done = true;
        if (done)
            jQuery('div.ms-toolpanefooter').append('<a href=\"#noop\" style=\"font-size: 11px; font-family: Arial, Helvetica, Sans-Serif; color: #047;\" onclick=\"jQuery(\'#roxfilterspecial\').hide();jQuery(\'#roxfilteradvanced\').hide(); jQuery(this).hide();\">' + jQuery('button#roxfiltappbtn').text() + ' / ' + jQuery('button#roxfiltdiscbtn').text() + ' &hellip;</a>');
    });
}

function toggleListWizard() {
    var $wiz = jQuery('#roxListWizard'), $hidden = jQuery('#' + roxHiddenFieldID), $link = jQuery('#roxListWizardLink'), linkText = $link.text();
    jQuery('#roxInfoFilterNames').slideToggle('fast');
    $wiz.slideToggle('fast', function () {
        if ($wiz.css('display') == 'block') {
            $hidden.val('1');
            $link.css('font-weight', 'bold').text(linkText.substr(0, linkText.length - 1) + ':').addClass('rox-info');
        } else {
            $hidden.val('');
            $link.removeClass('rox-info').css('font-weight', 'normal').text(linkText.substr(0, linkText.length - 1) + '?');
        }
    });
    location.replace('#roxtooltop');
}

function toolFilterAction(action, msg) {
    if ((!msg) || confirm(msg)) {
        setTimeout("showBusy();", 10);
        jQuery('#roxfilteraction').val(action);
        setTimeout("theForm.submit();", 500);
    }
}

function roxMultiAdd(pNum, id) {
    var multi = roxMultis[id];
    var autoGenericNum = roxMultiGetMaxNum(id) + 1;
    multi.cfg.push({ field: multi.defField, op: multi.defOp, lop: multi.defLop, val: '', num: autoGenericNum, parentNum: pNum, deep: 0, sort: 0 });
    roxMultiCfgReOrder(id, pNum);
    roxMultiUpdate(id);
    roxMultiRender(id);
}

function roxMultiGetMaxNum(id) {
    var multi = roxMultis[id];
    var maxNnum = 0;
    $.each(multi.cfg, function (i, item) {
        if (item.num > maxNnum) {
            maxNnum = item.num
        }
    });
    return maxNnum;
}

function roxMultiCfgReOrder(id, parent) {
    var multi = roxMultis[id];
    var cfg = multi.cfg;
    multi.cfg1 = [{ field: multi.defField, op: multi.defOp, lop: multi.defLop, val: '', num: 0, parentNum: -1, deep: 0, sort: 0}];
    multi.cfg1.splice(0, 1);
    multi.sortNum = 0;
    var deep = 0;
    roxMultiCfgRecurseOrder(-1, cfg, id, deep);
    multi.cfg = multi.cfg1;
}

function roxMultiCfgRecurseOrder(parentNum, arrCfg, id, deep) {
    var multi = roxMultis[id];
    arr = jQuery.grep(arrCfg, function (item) { return item.parentNum == parentNum; });
    if (arr.length > 0) {
        $.each(arr, function (i, item) {
            item.deep = deep;
            multi.cfg1.push(item);
            multi.sortNum++;
            roxMultiCfgRecurseOrder(item.num, arrCfg, id, deep + 1);
        });
    }

}

function roxMultiChanged(id, index, propName, propVal) {
    var multi = roxMultis[id], fieldType = (multi.fieldTypes ? multi.fieldTypes[multi.cfg[index].field] : '');
    multi.cfg[index][propName] = propVal;
    if ((fieldType == 'User') && (propName == 'op'))
        jQuery('#' + id + '_' + index + '_userpicker').css({ "visibility": (((propVal == "Me") || (propVal == "NotMe")) ? "hidden" : "visible") });
    roxMultiUpdate(id);
}

function roxMultiCheckEditorVals(id) {
    var i, m, ppVal, dtInputs = jQuery('span.rox-multi-datepicker table.rox-dtpickertable td.ms-dtinput input.ms-input'), dtID, ppID, preID, midm, index, multi, $dtInput, $ppInput, $tmp, tmp, ppInputs = jQuery('span.rox-multi-userpicker span.ms-input table.ms-input table div.ms-inputuserfield');
    for (i = 0; i < dtInputs.length; i++) {
        $dtInput = jQuery(dtInputs[i]);
        dtID = dtInputs[i].id;
        preID = dtID.substr(0, dtID.indexOf('_DatePicker'));
        multi = null;
        mid = null;
        index = -1;
        for (m in roxMultis)
            if (roxMultis[m].ctlID == (preID + '_MultiTextBox')) {
                multi = roxMultis[mid = m];
                break;
            }
        if (multi && mid && ($tmp = $dtInput.parents('span.rox-multi-datepicker:first')).length && (tmp = $tmp[0]) && ((index = parseInt(tmp.id.substr(tmp.id.indexOf('_') + 1))) >= 0))
            roxMultiChanged(mid, index, 'val', $dtInput.val());
    }
    for (i = 0; i < ppInputs.length; i++) {
        $ppInput = jQuery(ppInputs[i]);
        ppVal = $ppInput.find('span#content:first').text();
        ppID = $ppInput.parents('td:first')[0].id;
        preID = ppID.substr(0, ppID.indexOf('_PeoplePicker'));
        multi = null;
        mid = null;
        index = -1;
        for (m in roxMultis)
            if (roxMultis[m].ctlID == (preID + '_MultiTextBox')) {
                multi = roxMultis[mid = m];
                break;
            }
        if (multi && mid && ($tmp = $ppInput.parents('span.rox-multi-userpicker:first')).length && (tmp = $tmp[0]) && ((index = parseInt(tmp.id.substr(tmp.id.indexOf('_') + 1))) >= 0))
            roxMultiChanged(mid, index, 'val', jQuery.trim(ppVal));
    }
}

function roxMultiGetEditor(id, multi, index) {
    var exist = false, ret = {}, isAc, fieldType = (multi.fieldTypes ? multi.fieldTypes[multi.cfg[index].field] : ''), isNum = ((fieldType == 'Integer') || (fieldType == 'Number') || (fieldType == 'Counter'));
    if ((fieldType == 'DateTime') && ((exist = roxLastDateCtls[id + index]) || (roxMultiDateCounts[id] < 6))) {
        if (!exist)
            roxLastDateCtls[id + index] = roxMultiDateCounts[id] = roxMultiDateCounts[id] + 1;
        ret['html'] = ('<span class="rox-multi-datepicker" id="' + id + '_' + index + '_datepicker"></span> ');
        ret['func'] = function () {
            jQuery(document.getElementById(id + '_' + index + '_datepicker')).append(document.getElementById(multi.ctlID.substr(0, multi.ctlID.lastIndexOf('_')) + '_DatePicker' + (roxLastDateCtls[id + index] - 1)));
        };
    } else if ((fieldType == 'User') && ((exist = roxLastUserCtls[id + index]) || (roxMultiUserCounts[id] < 6))) {
        if (!exist)
            roxLastUserCtls[id + index] = roxMultiUserCounts[id] = roxMultiUserCounts[id] + 1;
        ret['html'] = ('<span class="rox-multi-userpicker" id="' + id + '_' + index + '_userpicker"></span> ');
        ret['func'] = function () {
            var el = document.getElementById(id + '_' + index + '_userpicker'), $el = jQuery(el), tmp;
            $el.append(document.getElementById(multi.ctlID.substr(0, multi.ctlID.lastIndexOf('_')) + '_PeoplePicker' + (roxLastUserCtls[id + index] - 1)));
            if (((tmp = multi.cfg[index]['op']) == 'Me') || (tmp == 'NotMe'))
                $el.css({ "visibility": "hidden" });
        };
    } else if (fieldType == 'Boolean') {
        ret['html'] = '<span class="rox-multi-check-input"><input id="' + id + '_' + index + '_checkbox" value="1" class="rox-multi-input" type="checkbox" checked="checked" disabled="disabled"/> <label for="' + id + '_' + index + '_checkbox">' + roxLox['MultiChecked'] + '</label></span> ';
        ret['func'] = function () {
            roxMultiChanged(id, index, 'val', '1');
        };
    } else {
        isAc = multi.autoComplete && multi.autoComplete.active && multi.cfg[index].field && ('*' != multi.cfg[index].field);
        ret['html'] = '<input id="' + id + '_' + index + '_textbox" class="ms-input rox-multi-input rox-multi-text-input" type="text" onchange="roxMultiChanged(\'' + id + '\', ' + index + ', \'val\', jQuery(this).val());"' + (isAc ? ' autocomplete="off" onblur="roxMultiChanged(\'' + id + '\', ' + index + ', \'val\', jQuery(this).val());"' : '') + '/> ';
        if (isAc)
            ret['func'] = function () {
                jQuery('#' + id + '_' + index + '_textbox').autocomplete(multi.autoComplete.url + '&f=' + multi.cfg[index].field, multi.autoComplete.opts);
            }
    }
    return ret; ;
}

function roxMultiInit(id) {
    jQuery(document).ready(function () {
        var multi = roxMultis[id], tb = document.getElementById(multi.ctlID), $tb = jQuery(tb), cfg, cfg1;
        try {
            cfg = jQuery.parseJSON($tb.text());
        } catch (e) {
            cfg = null;
        }
        if ((!cfg) || !cfg.length)
            cfg = [{ field: multi.defField, op: multi.defOp, lop: multi.defLop, val: '', num: 0, parentNum: -1, deep: 0, sort: 0}];
        $tb.text(JSON.stringify(multi.cfg = cfg));
        multi.cfg1 = cfg1;
        roxMultiRender(id);
        if (!roxDtInt)
            roxDtInt = setInterval("roxMultiCheckEditorVals('" + id + "');", 500);
    });
}

function roxMultiRemove(id, index) {
    var multi = roxMultis[id];
    var multicfg = multi.cfg
    var multicfg1 = jQuery.grep(multicfg, function (item) { return item.parentNum == multicfg[index].num; });
    roxMultiRecursive(multicfg1, multicfg[index]);
    multi.cfg.splice(index, 1);
    roxMultiUpdate(id);
    roxMultiRender(id);

}

function roxMultiRecursive(multicfg, item) {
    var itemTem = item;
    $.each(multicfg, function (i, item1) {
        if (item1.parentNum == itemTem.num) {
            var multicfg1 = jQuery.grep(multicfg, function (item2) { return item2.parentNum == item1.num; });
            roxMultiRecursive(multicfg1, item1);
            item1.parentNum = itemTem.parentNum;
            item1.deep = itemTem.deep;
        }
    });
}

function inputTipText() {
    var objs = jQuery("input[id*='_DatePicker']");
    objs.each(function () {
        jQuery(this).attr('title', '日期输入格式为：年/月/日，例如：2014/1/1')
    });
}

function roxMultiRender(id) {
    var i, theElem, editor, multi = roxMultis[id], cfg = multi.cfg, $div = jQuery('div.rox-multibox-' + id), tmp, html, ddls = ['field', 'op'];
    if (multi.allowMulti)
        ddls.push('lop');
    if (roxMultiDateCounts[id]) {
        for (i = 0; i < roxMultiDateCounts[id]; i++) {
            if ((theElem = document.getElementById(multi.ctlID.substr(0, multi.ctlID.lastIndexOf('_')) + '_DatePicker' + i)) != null) {
                //jQuery('#' + multi.ctlID.substr(0, multi.ctlID.lastIndexOf('_')) + '_DatePicker' + i).attr('title', 'test');
                //alert('#' + multi.ctlID.substr(0, multi.ctlID.lastIndexOf('_')) + '_DatePicker' + i);
                jQuery(document.getElementById(multi.ctlID.substr(0, multi.ctlID.lastIndexOf('_')) + '_MultiPanel')).append(theElem);
            }
            if (roxMultiUserCounts[id])
                for (i = 0; i < roxMultiUserCounts[id]; i++)
                    if ((theElem = document.getElementById(multi.ctlID.substr(0, multi.ctlID.lastIndexOf('_')) + '_PeoplePicker' + i)) != null)
                        jQuery(document.getElementById(multi.ctlID.substr(0, multi.ctlID.lastIndexOf('_')) + '_MultiPanel')).append(theElem);
        }
    }
    $div.html('');
    for (i = 0; i < cfg.length; i++) {
        html = '<div class="rox-multifilter rox-multifilter-' + id + '_' + i + '" style="padding-left: ' + cfg[i].deep * 20 + 'px;"><select class="rox-fieldsel" onchange="roxMultiChanged(\'' + id + '\', ' + i + ', \'field\', this.options[this.selectedIndex].value);roxMultiRender(\'' + id + '\');roxMultiChanged(\'' + id + '\', ' + i + ', \'op\', jQuery(\'.rox-opsel-' + id + '-' + i + '\')[0].options[jQuery(\'.rox-opsel-' + id + '-' + i + '\')[0].selectedIndex].value);">' + multi.fieldOpts + '</select> <select class="rox-opsel rox-opsel-' + id + '-' + i + '" onchange="roxMultiChanged(\'' + id + '\', ' + i + ', \'op\', this.options[this.selectedIndex].value);"' + (multi.isCaml ? '' : ' disabled="disabled"') + '>' + (multi.isCaml ? (((multi.fields && multi.fields[cfg[i].field]) ? multi.fields[cfg[i].field] : (multi.allowAnyAllOps ? roxMultiOpsAll : roxMultiOpsAny))) : roxMultiOpsNone) + '</select> ' + (editor = roxMultiGetEditor(id, multi, i, multi.autoComplete))['html'];
        if (multi.allowMulti) {
            if ((jQuery.grep(cfg, function (item) { return item.parentNum == cfg[i].parentNum; }))[0].num == cfg[i].num) {
                html += ('<select class="rox-lopsel" onchange="roxMultiChanged(\'' + id + '\', ' + i + ', \'lop\', this.options[this.selectedIndex].value);"' + ((i == (cfg.length - 1)) ? ' style="visibility: visible;"' : '') + (multi.isCaml ? '' : ' disabled="disabled"') + '><option value="And">' + roxLox['CamlOp_And'] + '</option><option value="Or">' + roxLox['CamlOp_Or'] + '</option></select>');
            }
            else {
                html += ('<select class="rox-lopsel" style="display:none;" onchange="roxMultiChanged(\'' + id + '\', ' + i + ', \'lop\', this.options[this.selectedIndex].value);"' + ((i == (cfg.length - 1)) ? ' style="visibility: hidden;"' : '') + (multi.isCaml ? '' : ' disabled="disabled"') + '><option value="And">' + roxLox['CamlOp_And'] + '</option><option value="Or">' + roxLox['CamlOp_Or'] + '</option></select>');
            }
            //html += ('<a href="#" onclick="roxMultiAdd(' + cfg[i].num + ',\'' + id + '\');"><img align="bottom" src="/_layouts/images/roxority_FilterZen/add.png"/></a><a href="#" onclick="roxMultiAdd(' + cfg[i].parentNum + ',\'' + id + '\');"><img align="bottom" src="/_layouts/images/roxority_FilterZen/add.png"/></a> ');
            html += ('<img src="/_layouts/images/roxority_FilterZen/add.png" style="margin-left:5px;vertical-align:middle;" class="roxMultiAdd" alt="" title="<p style=\'margin: 0px; margin-bottom: 5px; \'><button type=\'button\' class=\'tooltipster-ow-btn tooltipster-ow-btn-success\' onclick=roxMultiAdd(\'' + cfg[i].parentNum + '\',\'' + id + '\'); style=\'width:60px; font-size: 12px; padding: 0px;\' >条件</button></p><p style=\'margin: 0px;\'><button type=\'button\' class=\'tooltipster-ow-btn tooltipster-ow-btn-success\'  onclick=roxMultiAdd(\'' + cfg[i].num + '\',\'' + id + '\'); style=\'width:60px; font-size: 12px; padding: 0px; \'>子条件</button></p>">');
            if (cfg.length > 1)
                html += '<a href="#" onclick="roxMultiRemove(\'' + id + '\', ' + i + ');"><img style="vertical-align:middle;" align="bottom" src="/_layouts/images/roxority_FilterZen/remove.gif"/></a> ';
        }
        $div.append(html + '</div>');
        if (editor['func'])
            setTimeout(editor['func'], (i + 1));

        for (var d = 0; d < ddls.length; d++)
            if ((cfg[i][ddls[d]]) && ((ddls[d] == 'field') || (multi.isCaml)))
                for (var j = 0; j < (tmp = jQuery('div.rox-multibox-' + id + ' div.rox-multifilter-' + id + '_' + i + ' select.rox-' + ddls[d] + 'sel')[0]).options.length; j++)
                    if (tmp.options[j].value == cfg[i][ddls[d]]) {
                        tmp.selectedIndex = j;
                        break;
                    }
        jQuery('div.rox-multibox-' + id + ' div.rox-multifilter-' + id + '_' + i + ' input.rox-multi-text-input').val(cfg[i].val);
    }
    changeOptionText();
    //modified by lhan MultiAdd img tooltip
    $('.roxMultiAdd').tooltipster({
        animation: 'fade',
        arrow: true,
        theme: '.tooltipster-shadow',
        position: 'right',
        interactive: true
    });
    inputTipText();
}

function roxMultiUpdate(id) {
    var multi = roxMultis[id], tb = document.getElementById(multi.ctlID);
    jQuery(tb).text(JSON.stringify(multi.cfg));
}


//modified by lhan
function changeOptionText()
{
 var options = document.getElementsByTagName('option');
 var i = 0;
 for (; i < options.length; i++)
 {
  var option = options[i];
  switch(option.value)
  {
  
  case 'Eq':
   option.text = "等于";
   //$(option).parent().attr('disabled', true);
   break;
  case 'Neq':
   option.text = "不等于";
   //$(option).remove();
   //i = i - 1;
   break;
  case 'BeginsWith':
   option.text = "以XX开头";
   //$(option).remove();
   //i = i - 1;
   break;
  case 'begins with':
   option.text = "以XX开头";
   //$(option).remove();
   //i = i - 1;
   break;
  case 'Contains':
   option.text = "包含";
   //$(option).remove();
   //i = i - 1;
   break;
  case 'Gt':
   option.text = "大于";
   //$(option).remove();
   //i = i - 1;
   break;
  case 'Geq':
   option.text = "大于等于";
   //$(option).remove();
   //i = i - 1;
   break;
  case 'Lt':
   option.text = "小于";
   //$(option).remove();
   //i = i - 1;
   break;
  case 'Leq':
   option.text = "小于等于";
   //$(option).remove();
   //i = i - 1;
   break;
   
  case 'And':
   option.text = "并且";
   break;
  case 'Or':
   option.text = "或者";
   break;
  case 'kyxm_participant_text':
   option.text = '参与人';
   break;
  case 'kyxm_leader_text':
   option.text = '项目负责人';
   break;
  case 'sci_firstAuthor_text':
   option.text = '第一作者（中文）';
   break;
  case 'sci_secondAuthor_text':
   option.text = '通讯作者（中文）';
   break;
  case 'ei_firstAuthor_text':
   option.text = '第一作者（中文）';
   break;
  case 'zscq_inventor_text':
   option.text = '发明人';
   break;
  case 'zscq_applicant_text':
   option.text = '申请人';
   break;
  case 'jszr_leader_text':
   option.text = '项目负责人';
   break;
  case 'jsjzzq_author_text':
   option.text = '著作权人';
   break;
  case 'cgjl_participant_text':
   option.text = '完成人';
   break;
  case 'bkls_boKuanPiCi_text':
   option.text = '拨款批次号';
   break;
  case 'bkls_kyxmYear_number':
   option.text = '立项年度';
   break;
  case 'kyxm_year':
   option.text = '立项年度';
   break;
  case 'xmjf_year':
   option.text = '立项年度';
   break;
  case 'sci_year':
	option.text = '年度';
	break;
  case 'ei_year':
	option.text = '年度';
	break;
  case 'cgjl_year':
	option.text = '获奖年度';
	break;
  case 'jsjzzq_year_number':
	option.text = '取得年度';
	break;
  case 'wzzpjlyyx_year_number':
	option.text = '取得年度';
	break;
  default:
   break;
  }
 }
 i = 0;
 
 var optgroups = document.getElementsByTagName('optgroup');
 var k = 0;
 for (; k < optgroups.length; k++)
 {
  switch(optgroups[k].label)
  {
  case 'View fields':
   optgroups[k].label = '-----';
   break;
  case 'System fields':
   optgroups[k].label = '-----';
   break;
  case 'List fields':
   optgroups[k].label = '-----';
   break;
  default:
   break;
  }
 }
 k = 0;
}
